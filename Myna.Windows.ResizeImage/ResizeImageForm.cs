using Myna.Windows.ResizeImage.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myna.Windows.ResizeImage
{
	public partial class ResizeImageForm : Form
	{
		public ResizeImageForm()
		{
			InitializeComponent();
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			widthTextBox.Text = Settings.Default.width.ToString();
			heightTextBox.Text = Settings.Default.height.ToString();
			inputFilesDialog.InitialDirectory = Settings.Default.inputFilesDir;
			outputDirTextBox.Text = Settings.Default.outputDir;
			outputDirDialog.SelectedPath = Settings.Default.outputDir;
		}

		private void OnResizeBtnClick(object sender, EventArgs e)
		{
			string fileNames = inputFilesTextBox.Text;
			if (string.IsNullOrEmpty(fileNames))
			{
				Console.WriteLine("string.IsNullOrEmpty(fileNames)");
				return;
			}

			var files = fileNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < files.Length; i++)
			{
				string file = files[i].Trim();
				int result = ResizeImage(file);
				if (result != 0)
				{
					Console.WriteLine("error " + result);
					return;
				}
			}

			string outputDir = Settings.Default.outputDir;
			Process.Start("explorer.exe", outputDir);
		}

		private void OnOpenInputFilesBtnClick(object sender, EventArgs e)
		{
			var result = inputFilesDialog.ShowDialog();
			if (result != DialogResult.OK)
			{
				return;
			}

			var files = inputFilesDialog.FileNames;
			inputFilesTextBox.Text = string.Join(",", files);

			string file = inputFilesDialog.FileNames[0];
			string dir = Path.GetDirectoryName(file);
			Settings.Default.inputFilesDir = dir;
			Settings.Default.Save();
		}

		private void OnOpenOutputDirBtnClick(object sender, EventArgs e)
		{
			var result = outputDirDialog.ShowDialog();
			if (result != DialogResult.OK)
			{
				return;
			}

			string dir = outputDirDialog.SelectedPath;
			outputDirTextBox.Text = dir;
			Settings.Default.outputDir = dir;
			Settings.Default.Save();
		}

		private void OnWidthInputChanged(object sender, EventArgs e)
		{
			string input = widthTextBox.Text;
			if (!int.TryParse(input, out int value))
			{
				widthTextBox.Text = Settings.Default.width.ToString();
				return;
			}

			Settings.Default.width = value;
			Settings.Default.Save();
		}

		private void OnHeightInputChanged(object sender, EventArgs e)
		{
			string input = heightTextBox.Text;
			if (!int.TryParse(input, out int value))
			{
				heightTextBox.Text = Settings.Default.height.ToString();
				return;
			}

			Settings.Default.height = value;
			Settings.Default.Save();
		}

		// https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
		private static int ResizeImage(string inputFile)
		{
			if (!File.Exists(inputFile))
			{
				Console.WriteLine("!File.Exists(inputFile)");
				return 1;
			}

			string outputDir = Settings.Default.outputDir;
			if (string.IsNullOrEmpty(outputDir))
			{
				Console.WriteLine("string.IsNullOrEmpty(outputDir)");
				return 1;
			}

			if (Directory.Exists(outputDir))
			{
				try
				{
					Directory.CreateDirectory(outputDir);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					return 1;
				}
			}

			int width = Settings.Default.width;
			if (width < 1)
			{
				Console.WriteLine("width < 1");
				return 1;
			}

			int height = Settings.Default.height;
			if (height < 1)
			{
				Console.WriteLine("width < 1");
				return 1;
			}

			var image = Image.FromFile(inputFile);
			if (image == null)
			{
				Console.WriteLine("image == null: " + inputFile);
				return 1;
			}

			int srcWidth = image.Width;
			int srcHeight = image.Height;

			double widthFactor = (double)srcWidth / width;
			double heightFactor = (double)srcHeight / height;

			bool TryGetFillResizeDimensions(double factor, out int w, out int h)
			{
				w = CeilToInt(srcWidth / factor);
				h = CeilToInt(srcHeight / factor);
				return w >= width && h >= height;
			}

			//bool TryGetFitResizeDimensions(double factor, out int w, out int h)
			//{
			//	w = FloorToInt(srcWidth / factor);
			//	h = FloorToInt(srcHeight / factor);
			//	return w <= width && h <= height;
			//}

			if (!(TryGetFillResizeDimensions(widthFactor, out int destWidth, out int destHeight)
				|| TryGetFillResizeDimensions(heightFactor, out destWidth, out destHeight)))
			{
				Console.WriteLine("!TryGetResizeDimensions: " + inputFile);
				return 1;
			}


			// resize image
			var destRect = new Rectangle(0, 0, destWidth, destHeight);
			var destImage = new Bitmap(destWidth, destHeight);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var wrapMode = new ImageAttributes())
				{
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
				}
			}

			// save image
			string fileName = Path.GetFileNameWithoutExtension(inputFile);
			string ext = Path.GetExtension(inputFile);
			var sb = new StringBuilder();
			sb.Append(outputDir);
			sb.Append("\\");
			sb.Append(fileName);
			sb.Append("-");
			sb.Append(destWidth);
			sb.Append("-");
			sb.Append(destHeight);
			sb.Append(ext);
			string outputFile = sb.ToString();

			try
			{
				destImage.Save(outputFile, ImageFormat.Jpeg);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			Console.WriteLine($"resize {destWidth}, {destHeight}: {outputFile}, {image.RawFormat}");
			return 0;
		}

		private static int CeilToInt(double value)
		{
			value = Math.Ceiling(value);
			return Convert.ToInt32(value);
		}

		private static int FloorToInt(double value)
		{
			value = Math.Floor(value);
			return Convert.ToInt32(value);
		}
	}
}
