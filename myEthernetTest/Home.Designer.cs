namespace TrackMe
{
    partial class Home
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
            this.newtask = new System.Windows.Forms.Button();
            this.tasks = new System.Windows.Forms.Button();
            this.track = new System.Windows.Forms.Button();
            this.search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newtask
            // 
            this.newtask.Location = new System.Drawing.Point(12, 12);
            this.newtask.Name = "newtask";
            this.newtask.Size = new System.Drawing.Size(125, 115);
            this.newtask.TabIndex = 0;
            this.newtask.Text = "NEW";
            this.newtask.UseVisualStyleBackColor = true;
            // 
            // tasks
            // 
            this.tasks.Location = new System.Drawing.Point(144, 13);
            this.tasks.Name = "tasks";
            this.tasks.Size = new System.Drawing.Size(128, 114);
            this.tasks.TabIndex = 1;
            this.tasks.Text = "Tasks";
            this.tasks.UseVisualStyleBackColor = true;
            // 
            // track
            // 
            this.track.Location = new System.Drawing.Point(13, 134);
            this.track.Name = "track";
            this.track.Size = new System.Drawing.Size(124, 115);
            this.track.TabIndex = 2;
            this.track.Text = "Tracking";
            this.track.UseVisualStyleBackColor = true;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(144, 134);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(128, 115);
            this.search.TabIndex = 3;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.search);
            this.Controls.Add(this.track);
            this.Controls.Add(this.tasks);
            this.Controls.Add(this.newtask);
            this.Name = "Home";
            this.Text = "Home";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newtask;
        private System.Windows.Forms.Button tasks;
        private System.Windows.Forms.Button track;
        private System.Windows.Forms.Button search;
    }
}