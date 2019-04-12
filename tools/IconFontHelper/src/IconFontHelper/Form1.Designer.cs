namespace IconFontHelper
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
            this.tlpGrid = new System.Windows.Forms.TableLayoutPanel();
            this.btnUpdateList = new System.Windows.Forms.Button();
            this.btnGenerateBatch = new System.Windows.Forms.Button();
            this.pbActivity = new System.Windows.Forms.ProgressBar();
            this.tlpGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpGrid
            // 
            this.tlpGrid.ColumnCount = 3;
            this.tlpGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlpGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpGrid.Controls.Add(this.btnUpdateList, 1, 1);
            this.tlpGrid.Controls.Add(this.btnGenerateBatch, 1, 3);
            this.tlpGrid.Controls.Add(this.pbActivity, 1, 5);
            this.tlpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGrid.Location = new System.Drawing.Point(0, 0);
            this.tlpGrid.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.tlpGrid.Name = "tlpGrid";
            this.tlpGrid.RowCount = 6;
            this.tlpGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpGrid.Size = new System.Drawing.Size(1271, 861);
            this.tlpGrid.TabIndex = 0;
            // 
            // btnUpdateList
            // 
            this.btnUpdateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdateList.Location = new System.Drawing.Point(127, 129);
            this.btnUpdateList.Margin = new System.Windows.Forms.Padding(0);
            this.btnUpdateList.Name = "btnUpdateList";
            this.btnUpdateList.Size = new System.Drawing.Size(1016, 215);
            this.btnUpdateList.TabIndex = 5;
            this.btnUpdateList.Text = "Update SVG List";
            this.btnUpdateList.UseVisualStyleBackColor = true;
            this.btnUpdateList.Click += new System.EventHandler(this.btnUpdateList_Click);
            // 
            // btnGenerateBatch
            // 
            this.btnGenerateBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGenerateBatch.Location = new System.Drawing.Point(127, 473);
            this.btnGenerateBatch.Margin = new System.Windows.Forms.Padding(0);
            this.btnGenerateBatch.Name = "btnGenerateBatch";
            this.btnGenerateBatch.Size = new System.Drawing.Size(1016, 215);
            this.btnGenerateBatch.TabIndex = 6;
            this.btnGenerateBatch.Text = "Generate Batch";
            this.btnGenerateBatch.UseVisualStyleBackColor = true;
            this.btnGenerateBatch.Click += new System.EventHandler(this.btnGenerateBatch_Click);
            // 
            // pbActivity
            // 
            this.pbActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbActivity.Location = new System.Drawing.Point(130, 820);
            this.pbActivity.Name = "pbActivity";
            this.pbActivity.Size = new System.Drawing.Size(1010, 38);
            this.pbActivity.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 861);
            this.Controls.Add(this.tlpGrid);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "Form1";
            this.Text = "Icon Font Helper";
            this.tlpGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpGrid;
        private System.Windows.Forms.Button btnUpdateList;
        private System.Windows.Forms.Button btnGenerateBatch;
        private System.Windows.Forms.ProgressBar pbActivity;
    }
}

