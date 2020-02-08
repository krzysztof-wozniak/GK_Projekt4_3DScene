namespace GK_Projekt4_3DScene
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lightModelGroupBox = new System.Windows.Forms.GroupBox();
            this.phongLightRadioButton = new System.Windows.Forms.RadioButton();
            this.gouraudLightRadioButton = new System.Windows.Forms.RadioButton();
            this.constantLightRadioButton = new System.Windows.Forms.RadioButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.fpsTimer = new System.Windows.Forms.Timer(this.components);
            this.mainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.lightModelGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.mainTableLayoutPanel.Controls.Add(this.pictureBox, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.fpsLabel, 1, 1);
            this.mainTableLayoutPanel.Controls.Add(this.label1, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 2;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.44444F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.55556F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(824, 561);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(669, 490);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.Location = new System.Drawing.Point(678, 496);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(33, 13);
            this.fpsLabel.TabIndex = 2;
            this.fpsLabel.Text = "FPS: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 496);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lightModelGroupBox, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(678, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(143, 274);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lightModelGroupBox
            // 
            this.lightModelGroupBox.Controls.Add(this.phongLightRadioButton);
            this.lightModelGroupBox.Controls.Add(this.gouraudLightRadioButton);
            this.lightModelGroupBox.Controls.Add(this.constantLightRadioButton);
            this.lightModelGroupBox.Location = new System.Drawing.Point(3, 3);
            this.lightModelGroupBox.Name = "lightModelGroupBox";
            this.lightModelGroupBox.Size = new System.Drawing.Size(137, 126);
            this.lightModelGroupBox.TabIndex = 0;
            this.lightModelGroupBox.TabStop = false;
            this.lightModelGroupBox.Text = "Light model";
            // 
            // phongLightRadioButton
            // 
            this.phongLightRadioButton.AutoSize = true;
            this.phongLightRadioButton.Location = new System.Drawing.Point(7, 81);
            this.phongLightRadioButton.Name = "phongLightRadioButton";
            this.phongLightRadioButton.Size = new System.Drawing.Size(96, 17);
            this.phongLightRadioButton.TabIndex = 2;
            this.phongLightRadioButton.TabStop = true;
            this.phongLightRadioButton.Text = "Phong shading";
            this.phongLightRadioButton.UseVisualStyleBackColor = true;
            this.phongLightRadioButton.CheckedChanged += new System.EventHandler(this.phongLightRadioButton_CheckedChanged);
            // 
            // gouraudLightRadioButton
            // 
            this.gouraudLightRadioButton.AutoSize = true;
            this.gouraudLightRadioButton.Location = new System.Drawing.Point(7, 57);
            this.gouraudLightRadioButton.Name = "gouraudLightRadioButton";
            this.gouraudLightRadioButton.Size = new System.Drawing.Size(106, 17);
            this.gouraudLightRadioButton.TabIndex = 1;
            this.gouraudLightRadioButton.TabStop = true;
            this.gouraudLightRadioButton.Text = "Gouraud shading";
            this.gouraudLightRadioButton.UseVisualStyleBackColor = true;
            this.gouraudLightRadioButton.CheckedChanged += new System.EventHandler(this.gouraudLightRadioButton_CheckedChanged);
            // 
            // constantLightRadioButton
            // 
            this.constantLightRadioButton.AutoSize = true;
            this.constantLightRadioButton.Location = new System.Drawing.Point(7, 33);
            this.constantLightRadioButton.Name = "constantLightRadioButton";
            this.constantLightRadioButton.Size = new System.Drawing.Size(67, 17);
            this.constantLightRadioButton.TabIndex = 0;
            this.constantLightRadioButton.TabStop = true;
            this.constantLightRadioButton.Text = "Constant";
            this.constantLightRadioButton.UseVisualStyleBackColor = true;
            this.constantLightRadioButton.CheckedChanged += new System.EventHandler(this.constantLightRadioButton_CheckedChanged);
            // 
            // timer
            // 
            this.timer.Interval = 30;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // fpsTimer
            // 
            this.fpsTimer.Interval = 1000;
            this.fpsTimer.Tick += new System.EventHandler(this.fpsTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 561);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.lightModelGroupBox.ResumeLayout(false);
            this.lightModelGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer fpsTimer;
        private System.Windows.Forms.Label fpsLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox lightModelGroupBox;
        private System.Windows.Forms.RadioButton phongLightRadioButton;
        private System.Windows.Forms.RadioButton gouraudLightRadioButton;
        private System.Windows.Forms.RadioButton constantLightRadioButton;
    }
}

