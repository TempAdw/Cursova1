namespace Cursova
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            GeneratePoints = new Button();
            PointsAmountBox = new TextBox();
            TextBoxReuslt2 = new TextBox();
            ButtonLines = new Button();
            TextBoxReuslt = new TextBox();
            label2 = new Label();
            ButtonClear = new Button();
            ButtonDeletePoint = new Button();
            ButtonAddPoint = new Button();
            ButtonSolveSim = new Button();
            ButtonSolveNearest = new Button();
            label1 = new Label();
            ButtonSolveGreedy = new Button();
            ButtonSave = new Button();
            ButtonLoad = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(GeneratePoints);
            splitContainer1.Panel2.Controls.Add(PointsAmountBox);
            splitContainer1.Panel2.Controls.Add(TextBoxReuslt2);
            splitContainer1.Panel2.Controls.Add(ButtonLines);
            splitContainer1.Panel2.Controls.Add(TextBoxReuslt);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(ButtonClear);
            splitContainer1.Panel2.Controls.Add(ButtonDeletePoint);
            splitContainer1.Panel2.Controls.Add(ButtonAddPoint);
            splitContainer1.Panel2.Controls.Add(ButtonSolveSim);
            splitContainer1.Panel2.Controls.Add(ButtonSolveNearest);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(ButtonSolveGreedy);
            splitContainer1.Panel2.Controls.Add(ButtonSave);
            splitContainer1.Panel2.Controls.Add(ButtonLoad);
            splitContainer1.Size = new Size(884, 600);
            splitContainer1.SplitterDistance = 600;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Beige;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(600, 600);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseClick += PictureBox1_MouseClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ImageAlign = ContentAlignment.TopLeft;
            label3.Location = new Point(3, 359);
            label3.Name = "label3";
            label3.Size = new Size(206, 15);
            label3.TabIndex = 19;
            label3.Text = "Enter the amount of random points:";
            // 
            // GeneratePoints
            // 
            GeneratePoints.Anchor = AnchorStyles.Top;
            GeneratePoints.Location = new Point(71, 406);
            GeneratePoints.Name = "GeneratePoints";
            GeneratePoints.Size = new Size(137, 30);
            GeneratePoints.TabIndex = 18;
            GeneratePoints.Text = "Generate points";
            GeneratePoints.UseVisualStyleBackColor = true;
            GeneratePoints.Click += GeneratePoints_Click;
            // 
            // PointsAmountBox
            // 
            PointsAmountBox.Location = new Point(2, 377);
            PointsAmountBox.Name = "PointsAmountBox";
            PointsAmountBox.Size = new Size(273, 23);
            PointsAmountBox.TabIndex = 17;
            // 
            // TextBoxReuslt2
            // 
            TextBoxReuslt2.Location = new Point(3, 289);
            TextBoxReuslt2.Name = "TextBoxReuslt2";
            TextBoxReuslt2.ReadOnly = true;
            TextBoxReuslt2.Size = new Size(273, 23);
            TextBoxReuslt2.TabIndex = 16;
            // 
            // ButtonLines
            // 
            ButtonLines.Anchor = AnchorStyles.Top;
            ButtonLines.Location = new Point(72, 317);
            ButtonLines.Name = "ButtonLines";
            ButtonLines.Size = new Size(137, 30);
            ButtonLines.TabIndex = 11;
            ButtonLines.Text = "Show Lines";
            ButtonLines.UseVisualStyleBackColor = true;
            ButtonLines.Click += buttonDrawLines;
            // 
            // TextBoxReuslt
            // 
            TextBoxReuslt.Location = new Point(3, 264);
            TextBoxReuslt.Name = "TextBoxReuslt";
            TextBoxReuslt.ReadOnly = true;
            TextBoxReuslt.Size = new Size(273, 23);
            TextBoxReuslt.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ImageAlign = ContentAlignment.TopLeft;
            label2.Location = new Point(3, 246);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 9;
            label2.Text = "Result:";
            // 
            // ButtonClear
            // 
            ButtonClear.Anchor = AnchorStyles.Top;
            ButtonClear.Location = new Point(71, 88);
            ButtonClear.Name = "ButtonClear";
            ButtonClear.Size = new Size(137, 30);
            ButtonClear.TabIndex = 8;
            ButtonClear.Text = "Clear canvas";
            ButtonClear.UseVisualStyleBackColor = true;
            ButtonClear.Click += buttonClear;
            // 
            // ButtonDeletePoint
            // 
            ButtonDeletePoint.Anchor = AnchorStyles.Top;
            ButtonDeletePoint.BackColor = SystemColors.ControlDark;
            ButtonDeletePoint.Location = new Point(140, 52);
            ButtonDeletePoint.Name = "ButtonDeletePoint";
            ButtonDeletePoint.Size = new Size(137, 30);
            ButtonDeletePoint.TabIndex = 7;
            ButtonDeletePoint.Text = "Delete point";
            ButtonDeletePoint.UseVisualStyleBackColor = true;
            ButtonDeletePoint.Click += buttonDeletePoint_Click;
            // 
            // ButtonAddPoint
            // 
            ButtonAddPoint.Anchor = AnchorStyles.Top;
            ButtonAddPoint.BackColor = SystemColors.ControlDark;
            ButtonAddPoint.Location = new Point(3, 52);
            ButtonAddPoint.Name = "ButtonAddPoint";
            ButtonAddPoint.Size = new Size(137, 30);
            ButtonAddPoint.TabIndex = 6;
            ButtonAddPoint.Text = "Add point";
            ButtonAddPoint.UseVisualStyleBackColor = true;
            ButtonAddPoint.Click += buttonAddPoint_Click;
            // 
            // ButtonSolveSim
            // 
            ButtonSolveSim.Location = new Point(2, 213);
            ButtonSolveSim.Name = "ButtonSolveSim";
            ButtonSolveSim.Size = new Size(274, 30);
            ButtonSolveSim.TabIndex = 5;
            ButtonSolveSim.Text = "Simulated annealing algorithm";
            ButtonSolveSim.UseVisualStyleBackColor = true;
            ButtonSolveSim.Click += buttonSolveSim;
            // 
            // ButtonSolveNearest
            // 
            ButtonSolveNearest.Location = new Point(2, 177);
            ButtonSolveNearest.Name = "ButtonSolveNearest";
            ButtonSolveNearest.Size = new Size(274, 30);
            ButtonSolveNearest.TabIndex = 4;
            ButtonSolveNearest.Text = "Nearest neighbour algorithm";
            ButtonSolveNearest.UseVisualStyleBackColor = true;
            ButtonSolveNearest.Click += buttonSolveNearest;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ImageAlign = ContentAlignment.TopLeft;
            label1.Location = new Point(2, 123);
            label1.Name = "label1";
            label1.Size = new Size(139, 15);
            label1.TabIndex = 3;
            label1.Text = "Choose solving method:";
            // 
            // ButtonSolveGreedy
            // 
            ButtonSolveGreedy.Location = new Point(2, 141);
            ButtonSolveGreedy.Name = "ButtonSolveGreedy";
            ButtonSolveGreedy.Size = new Size(274, 30);
            ButtonSolveGreedy.TabIndex = 2;
            ButtonSolveGreedy.Text = "Greedy algorithm";
            ButtonSolveGreedy.UseVisualStyleBackColor = true;
            ButtonSolveGreedy.Click += buttonSolveGreedy;
            // 
            // ButtonSave
            // 
            ButtonSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonSave.Location = new Point(140, 3);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(137, 30);
            ButtonSave.TabIndex = 1;
            ButtonSave.Text = "Save Graph";
            ButtonSave.UseVisualStyleBackColor = true;
            ButtonSave.Click += ButtonSave_Click;
            // 
            // ButtonLoad
            // 
            ButtonLoad.Location = new Point(3, 3);
            ButtonLoad.Name = "ButtonLoad";
            ButtonLoad.Size = new Size(137, 30);
            ButtonLoad.TabIndex = 0;
            ButtonLoad.Text = "Load Graph";
            ButtonLoad.UseVisualStyleBackColor = true;
            ButtonLoad.Click += buttonLoad_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 600);
            Controls.Add(splitContainer1);
            MaximumSize = new Size(900, 639);
            MinimumSize = new Size(900, 639);
            Name = "Form1";
            Text = "TSP solver";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Button ButtonSave;
        private Button ButtonLoad;
        private Button ButtonSolveGreedy;
        private Label label1;
        private Button ButtonSolveSim;
        private Button ButtonSolveNearest;
        public PictureBox pictureBox1;
        public Button ButtonAddPoint;
        private Button ButtonClear;
        public Button ButtonDeletePoint;
        private TextBox TextBoxReuslt;
        private Label label2;
        private Button ButtonLines;
        private TextBox TextBoxReuslt2;
        private TextBox PointsAmountBox;
        private Label label3;
        private Button GeneratePoints;
    }
}
