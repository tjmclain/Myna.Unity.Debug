namespace Myna.Windows.ResizeImage
{
	partial class ResizeImageForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.heightTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.widthTextBox = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.resizeBtn = new System.Windows.Forms.Button();
			this.inputFilesDialog = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.inputFilesTextBox = new System.Windows.Forms.TextBox();
			this.outputDirTextBox = new System.Windows.Forms.TextBox();
			this.openInputFilesBtn = new System.Windows.Forms.Button();
			this.openOutputDirBtn = new System.Windows.Forms.Button();
			this.outputDirDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// heightTextBox
			// 
			this.heightTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.heightTextBox.Location = new System.Drawing.Point(55, 40);
			this.heightTextBox.Name = "heightTextBox";
			this.heightTextBox.Size = new System.Drawing.Size(142, 20);
			this.heightTextBox.TabIndex = 6;
			this.heightTextBox.Text = "1080";
			this.heightTextBox.TextChanged += new System.EventHandler(this.OnHeightInputChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Width";
			// 
			// widthTextBox
			// 
			this.widthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.widthTextBox.Location = new System.Drawing.Point(55, 6);
			this.widthTextBox.Name = "widthTextBox";
			this.widthTextBox.Size = new System.Drawing.Size(142, 20);
			this.widthTextBox.TabIndex = 5;
			this.widthTextBox.Text = "1920";
			this.widthTextBox.TextChanged += new System.EventHandler(this.OnWidthInputChanged);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74F));
			this.tableLayoutPanel1.Controls.Add(this.heightTextBox, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.widthTextBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 85);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 67);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Height";
			// 
			// resizeBtn
			// 
			this.resizeBtn.Location = new System.Drawing.Point(12, 158);
			this.resizeBtn.Name = "resizeBtn";
			this.resizeBtn.Size = new System.Drawing.Size(75, 23);
			this.resizeBtn.TabIndex = 7;
			this.resizeBtn.Text = "Resize";
			this.resizeBtn.UseVisualStyleBackColor = true;
			this.resizeBtn.Click += new System.EventHandler(this.OnResizeBtnClick);
			// 
			// inputFilesDialog
			// 
			this.inputFilesDialog.Filter = "Images(*.jpg; *.png)| *.jpg;*.png|All files(*.*)|*.*";
			this.inputFilesDialog.Multiselect = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.56627F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.43374F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
			this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.inputFilesTextBox, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.outputDirTextBox, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.openInputFilesBtn, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.openOutputDirBtn, 2, 1);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(435, 67);
			this.tableLayoutPanel2.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(101, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Output Directory";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Input File(s)";
			// 
			// inputFilesTextBox
			// 
			this.inputFilesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.inputFilesTextBox.Location = new System.Drawing.Point(110, 6);
			this.inputFilesTextBox.Name = "inputFilesTextBox";
			this.inputFilesTextBox.Size = new System.Drawing.Size(226, 20);
			this.inputFilesTextBox.TabIndex = 1;
			// 
			// outputDirTextBox
			// 
			this.outputDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.outputDirTextBox.Location = new System.Drawing.Point(110, 40);
			this.outputDirTextBox.Name = "outputDirTextBox";
			this.outputDirTextBox.Size = new System.Drawing.Size(226, 20);
			this.outputDirTextBox.TabIndex = 3;
			// 
			// openInputFilesBtn
			// 
			this.openInputFilesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.openInputFilesBtn.Location = new System.Drawing.Point(342, 5);
			this.openInputFilesBtn.Name = "openInputFilesBtn";
			this.openInputFilesBtn.Size = new System.Drawing.Size(90, 23);
			this.openInputFilesBtn.TabIndex = 2;
			this.openInputFilesBtn.Text = "Browse";
			this.openInputFilesBtn.UseVisualStyleBackColor = true;
			this.openInputFilesBtn.Click += new System.EventHandler(this.OnOpenInputFilesBtnClick);
			// 
			// openOutputDirBtn
			// 
			this.openOutputDirBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.openOutputDirBtn.Location = new System.Drawing.Point(342, 38);
			this.openOutputDirBtn.Name = "openOutputDirBtn";
			this.openOutputDirBtn.Size = new System.Drawing.Size(90, 23);
			this.openOutputDirBtn.TabIndex = 4;
			this.openOutputDirBtn.Text = "Browse";
			this.openOutputDirBtn.UseVisualStyleBackColor = true;
			this.openOutputDirBtn.Click += new System.EventHandler(this.OnOpenOutputDirBtnClick);
			// 
			// outputDirDialog
			// 
			this.outputDirDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			// 
			// ResizeImageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(459, 340);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.resizeBtn);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ResizeImageForm";
			this.Text = "Resize Image";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox heightTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox widthTextBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button resizeBtn;
		private System.Windows.Forms.OpenFileDialog inputFilesDialog;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox inputFilesTextBox;
		private System.Windows.Forms.TextBox outputDirTextBox;
		private System.Windows.Forms.Button openInputFilesBtn;
		private System.Windows.Forms.Button openOutputDirBtn;
		private System.Windows.Forms.FolderBrowserDialog outputDirDialog;
	}
}

