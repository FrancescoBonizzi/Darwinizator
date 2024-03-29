﻿namespace GUI
{
    partial class GetEvolution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetEvolution));
            this.label2 = new System.Windows.Forms.Label();
            this.txtHerbivore = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCarnivorous = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtGeneralInfos = new System.Windows.Forms.RichTextBox();
            this.flagPause = new System.Windows.Forms.CheckBox();
            this.flagDebug = new System.Windows.Forms.CheckBox();
            this.flagRendering = new System.Windows.Forms.CheckBox();
            this.chartCarnivorousEvolution = new LiveCharts.WinForms.CartesianChart();
            this.chartHerbivoreEvolution = new LiveCharts.WinForms.CartesianChart();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 304);
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
            this.txtHerbivore.Size = new System.Drawing.Size(264, 161);
            this.txtHerbivore.TabIndex = 3;
            this.txtHerbivore.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(281, 304);
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
            this.txtCarnivorous.Size = new System.Drawing.Size(264, 161);
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
            // flagPause
            // 
            this.flagPause.Appearance = System.Windows.Forms.Appearance.Button;
            this.flagPause.AutoSize = true;
            this.flagPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagPause.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flagPause.Location = new System.Drawing.Point(352, 138);
            this.flagPause.Name = "flagPause";
            this.flagPause.Size = new System.Drawing.Size(52, 23);
            this.flagPause.TabIndex = 13;
            this.flagPause.Text = "Pause";
            this.flagPause.UseVisualStyleBackColor = true;
            this.flagPause.CheckedChanged += new System.EventHandler(this.FlagPause_CheckedChanged);
            // 
            // flagDebug
            // 
            this.flagDebug.Appearance = System.Windows.Forms.Appearance.Button;
            this.flagDebug.AutoSize = true;
            this.flagDebug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagDebug.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flagDebug.Location = new System.Drawing.Point(410, 138);
            this.flagDebug.Name = "flagDebug";
            this.flagDebug.Size = new System.Drawing.Size(54, 23);
            this.flagDebug.TabIndex = 14;
            this.flagDebug.Text = "Debug";
            this.flagDebug.UseVisualStyleBackColor = true;
            this.flagDebug.CheckedChanged += new System.EventHandler(this.FlagDebug_CheckedChanged);
            // 
            // flagRendering
            // 
            this.flagRendering.Appearance = System.Windows.Forms.Appearance.Button;
            this.flagRendering.AutoSize = true;
            this.flagRendering.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagRendering.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagRendering.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flagRendering.Location = new System.Drawing.Point(470, 138);
            this.flagRendering.Name = "flagRendering";
            this.flagRendering.Size = new System.Drawing.Size(75, 23);
            this.flagRendering.TabIndex = 15;
            this.flagRendering.Text = "Rendering";
            this.flagRendering.UseVisualStyleBackColor = true;
            this.flagRendering.CheckedChanged += new System.EventHandler(this.FlagRendering_CheckedChanged);
            // 
            // chartCarnivorousEvolution
            // 
            this.chartCarnivorousEvolution.BackColor = System.Drawing.Color.White;
            this.chartCarnivorousEvolution.Location = new System.Drawing.Point(281, 546);
            this.chartCarnivorousEvolution.Name = "chartCarnivorousEvolution";
            this.chartCarnivorousEvolution.Size = new System.Drawing.Size(264, 227);
            this.chartCarnivorousEvolution.TabIndex = 16;
            this.chartCarnivorousEvolution.Text = "chartCarnivorousEvolution";
            // 
            // chartHerbivoreEvolution
            // 
            this.chartHerbivoreEvolution.BackColor = System.Drawing.Color.White;
            this.chartHerbivoreEvolution.Location = new System.Drawing.Point(11, 546);
            this.chartHerbivoreEvolution.Name = "chartHerbivoreEvolution";
            this.chartHerbivoreEvolution.Size = new System.Drawing.Size(264, 227);
            this.chartHerbivoreEvolution.TabIndex = 17;
            this.chartHerbivoreEvolution.Text = "chartHerbivoreEvolution";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 510);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 21);
            this.label4.TabIndex = 18;
            this.label4.Text = "Evolution";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Crimson;
            this.label5.Location = new System.Drawing.Point(281, 510);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 21);
            this.label5.TabIndex = 19;
            this.label5.Text = "Evolution";
            // 
            // Infos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(196)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(567, 785);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chartHerbivoreEvolution);
            this.Controls.Add(this.chartCarnivorousEvolution);
            this.Controls.Add(this.flagRendering);
            this.Controls.Add(this.flagDebug);
            this.Controls.Add(this.flagPause);
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
        private System.Windows.Forms.CheckBox flagPause;
        private System.Windows.Forms.CheckBox flagDebug;
        private System.Windows.Forms.CheckBox flagRendering;
        private LiveCharts.WinForms.CartesianChart chartCarnivorousEvolution;
        private LiveCharts.WinForms.CartesianChart chartHerbivoreEvolution;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}