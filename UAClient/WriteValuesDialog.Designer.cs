namespace Siemens.OpcUA.Client
{
    partial class WriteValuesDialog
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
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.WriteValueCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NodeIdCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CurrentValueCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStrip();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStripStatusLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(341, 4);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(179, 4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(260, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 24);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WriteValueCH,
            this.NodeIdCH,
            this.CurrentValueCH});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.LabelEdit = true;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(419, 210);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // WriteValueCH
            // 
            this.WriteValueCH.Text = "Write Value";
            this.WriteValueCH.Width = 87;
            // 
            // NodeIdCH
            // 
            this.NodeIdCH.Text = "NodeId";
            // 
            // CurrentValueCH
            // 
            this.CurrentValueCH.Text = "Current Value";
            this.CurrentValueCH.Width = 115;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonApply);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 179);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(419, 31);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.toolStripStatusLabel);
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(173, 27);
            this.panel2.TabIndex = 4;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.LinkVisited = true;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 24);
            this.toolStripLabel1.Text = "Status";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripStatusLabel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStripStatusLabel.Location = new System.Drawing.Point(0, 0);
            this.toolStripStatusLabel.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(173, 27);
            this.toolStripStatusLabel.Stretch = true;
            this.toolStripStatusLabel.TabIndex = 4;
            this.toolStripStatusLabel.Text = "Status";
            // 
            // WriteValuesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(419, 210);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView);
            this.MinimumSize = new System.Drawing.Size(290, 38);
            this.Name = "WriteValuesDialog";
            this.Text = "Write Values";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStripStatusLabel.ResumeLayout(false);
            this.toolStripStatusLabel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader WriteValueCH;
        private System.Windows.Forms.ColumnHeader NodeIdCH;
        private System.Windows.Forms.ColumnHeader CurrentValueCH;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStripStatusLabel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;


    }
}