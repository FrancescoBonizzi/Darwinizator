namespace GUI
{
    partial class Infos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Infos));
            this.label2 = new System.Windows.Forms.Label();
            this.txtHerbivore = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCarnivorous = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtGeneralInfos = new System.Windows.Forms.RichTextBox();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 308);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Herbivore";
            // 
            // txtHerbivore
            // 
            this.txtHerbivore.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHerbivore.Location = new System.Drawing.Point(11, 332);
            this.txtHerbivore.Name = "txtHerbivore";
            this.txtHerbivore.Size = new System.Drawing.Size(264, 208);
            this.txtHerbivore.TabIndex = 3;
            this.txtHerbivore.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(281, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Carnivorous";
            // 
            // txtCarnivorous
            // 
            this.txtCarnivorous.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCarnivorous.Location = new System.Drawing.Point(281, 332);
            this.txtCarnivorous.Name = "txtCarnivorous";
            this.txtCarnivorous.Size = new System.Drawing.Size(264, 208);
            this.txtCarnivorous.TabIndex = 5;
            this.txtCarnivorous.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(189, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 50);
            this.label3.TabIndex = 7;
            this.label3.Text = "Darwinizator";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-4, -7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(288, 168);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // txtGeneralInfos
            // 
            this.txtGeneralInfos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGeneralInfos.Location = new System.Drawing.Point(11, 182);
            this.txtGeneralInfos.Name = "txtGeneralInfos";
            this.txtGeneralInfos.Size = new System.Drawing.Size(534, 91);
            this.txtGeneralInfos.TabIndex = 10;
            this.txtGeneralInfos.Text = "";
            // 
            // timerRefresh
            // 
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Interval = 200;
            this.timerRefresh.Tick += new System.EventHandler(this.TimerRefresh_Tick);
            // 
            // Infos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(196)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(567, 561);
            this.Controls.Add(this.txtGeneralInfos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCarnivorous);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHerbivore);
            this.Name = "Infos";
            this.Text = "Infos";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtHerbivore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtCarnivorous;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox txtGeneralInfos;
        private System.Windows.Forms.Timer timerRefresh;
    }
}