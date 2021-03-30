
namespace NET3
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
            this.components = new System.ComponentModel.Container();
            this.RchtxtChat = new System.Windows.Forms.RichTextBox();
            this.BtnBroadcast = new System.Windows.Forms.Button();
            this.RchtxtLog = new System.Windows.Forms.RichTextBox();
            this.TxtboxName = new System.Windows.Forms.TextBox();
            this.PnlTop = new System.Windows.Forms.Panel();
            this.PanelChat = new System.Windows.Forms.Panel();
            this.TxtboxSend = new System.Windows.Forms.TextBox();
            this.TmrLog = new System.Windows.Forms.Timer(this.components);
            this.PnlTop.SuspendLayout();
            this.PanelChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // RchtxtChat
            // 
            this.RchtxtChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RchtxtChat.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RchtxtChat.Location = new System.Drawing.Point(0, 0);
            this.RchtxtChat.Name = "RchtxtChat";
            this.RchtxtChat.ReadOnly = true;
            this.RchtxtChat.Size = new System.Drawing.Size(208, 404);
            this.RchtxtChat.TabIndex = 0;
            this.RchtxtChat.Text = "";
            // 
            // BtnBroadcast
            // 
            this.BtnBroadcast.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnBroadcast.Location = new System.Drawing.Point(12, 5);
            this.BtnBroadcast.Name = "BtnBroadcast";
            this.BtnBroadcast.Size = new System.Drawing.Size(116, 35);
            this.BtnBroadcast.TabIndex = 1;
            this.BtnBroadcast.Text = "Broadcast";
            this.BtnBroadcast.UseVisualStyleBackColor = true;
            this.BtnBroadcast.Click += new System.EventHandler(this.BtnBroadcast_Click);
            // 
            // RchtxtLog
            // 
            this.RchtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RchtxtLog.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RchtxtLog.Location = new System.Drawing.Point(0, 0);
            this.RchtxtLog.Name = "RchtxtLog";
            this.RchtxtLog.ReadOnly = true;
            this.RchtxtLog.Size = new System.Drawing.Size(800, 450);
            this.RchtxtLog.TabIndex = 2;
            this.RchtxtLog.Text = "";
            // 
            // TxtboxName
            // 
            this.TxtboxName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TxtboxName.Location = new System.Drawing.Point(134, 5);
            this.TxtboxName.Name = "TxtboxName";
            this.TxtboxName.Size = new System.Drawing.Size(160, 36);
            this.TxtboxName.TabIndex = 3;
            // 
            // PnlTop
            // 
            this.PnlTop.Controls.Add(this.BtnBroadcast);
            this.PnlTop.Controls.Add(this.TxtboxName);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTop.Location = new System.Drawing.Point(0, 0);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(800, 46);
            this.PnlTop.TabIndex = 4;
            // 
            // PanelChat
            // 
            this.PanelChat.Controls.Add(this.TxtboxSend);
            this.PanelChat.Controls.Add(this.RchtxtChat);
            this.PanelChat.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelChat.Location = new System.Drawing.Point(0, 46);
            this.PanelChat.Name = "PanelChat";
            this.PanelChat.Size = new System.Drawing.Size(208, 404);
            this.PanelChat.TabIndex = 5;
            // 
            // TxtboxSend
            // 
            this.TxtboxSend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtboxSend.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TxtboxSend.Location = new System.Drawing.Point(0, 368);
            this.TxtboxSend.Name = "TxtboxSend";
            this.TxtboxSend.Size = new System.Drawing.Size(208, 36);
            this.TxtboxSend.TabIndex = 1;
            this.TxtboxSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtboxSend_KeyDown);
            // 
            // TmrLog
            // 
            this.TmrLog.Enabled = true;
            this.TmrLog.Interval = 500;
            this.TmrLog.Tick += new System.EventHandler(this.TmrLog_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PanelChat);
            this.Controls.Add(this.PnlTop);
            this.Controls.Add(this.RchtxtLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.PnlTop.ResumeLayout(false);
            this.PnlTop.PerformLayout();
            this.PanelChat.ResumeLayout(false);
            this.PanelChat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RchtxtChat;
        private System.Windows.Forms.Button BtnBroadcast;
        private System.Windows.Forms.RichTextBox RchtxtLog;
        private System.Windows.Forms.TextBox TxtboxName;
        private System.Windows.Forms.Panel PnlTop;
        private System.Windows.Forms.Panel PanelChat;
        private System.Windows.Forms.TextBox TxtboxSend;
        private System.Windows.Forms.Timer TmrLog;
    }
}

