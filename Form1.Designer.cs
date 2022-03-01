
namespace NewGame1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.roomView = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.MessageOut = new System.Windows.Forms.Label();
            this.listActions = new System.Windows.Forms.ListBox();
            this.actionPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mapPanel = new System.Windows.Forms.Panel();
            this.mapText = new System.Windows.Forms.Label();
            this.inventoryPanel = new System.Windows.Forms.Panel();
            this.craftingPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.inventoryText = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.mapPanel.SuspendLayout();
            this.inventoryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.roomView);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 302);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // roomView
            // 
            this.roomView.AutoSize = true;
            this.roomView.Location = new System.Drawing.Point(3, 0);
            this.roomView.Name = "roomView";
            this.roomView.Size = new System.Drawing.Size(35, 13);
            this.roomView.TabIndex = 0;
            this.roomView.Text = "label1";
            // 
            // MessageOut
            // 
            this.MessageOut.AutoSize = true;
            this.MessageOut.Location = new System.Drawing.Point(12, 464);
            this.MessageOut.Name = "MessageOut";
            this.MessageOut.Size = new System.Drawing.Size(35, 13);
            this.MessageOut.TabIndex = 1;
            this.MessageOut.Text = "label1";
            this.MessageOut.Paint += new System.Windows.Forms.PaintEventHandler(this.MessageOut_Paint);
            // 
            // listActions
            // 
            this.listActions.FormattingEnabled = true;
            this.listActions.Location = new System.Drawing.Point(285, 591);
            this.listActions.Name = "listActions";
            this.listActions.Size = new System.Drawing.Size(120, 95);
            this.listActions.TabIndex = 2;
            // 
            // actionPanel
            // 
            this.actionPanel.Location = new System.Drawing.Point(285, 420);
            this.actionPanel.Name = "actionPanel";
            this.actionPanel.Size = new System.Drawing.Size(200, 165);
            this.actionPanel.TabIndex = 4;
            // 
            // mapPanel
            // 
            this.mapPanel.Controls.Add(this.mapText);
            this.mapPanel.Location = new System.Drawing.Point(776, 12);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(275, 255);
            this.mapPanel.TabIndex = 5;
            this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPanel_Paint);
            // 
            // mapText
            // 
            this.mapText.AutoSize = true;
            this.mapText.Location = new System.Drawing.Point(3, 0);
            this.mapText.Name = "mapText";
            this.mapText.Size = new System.Drawing.Size(35, 13);
            this.mapText.TabIndex = 0;
            this.mapText.Text = "label1";
            // 
            // inventoryPanel
            // 
            this.inventoryPanel.Controls.Add(this.craftingPanel);
            this.inventoryPanel.Controls.Add(this.inventoryText);
            this.inventoryPanel.Location = new System.Drawing.Point(776, 273);
            this.inventoryPanel.Name = "inventoryPanel";
            this.inventoryPanel.Size = new System.Drawing.Size(275, 164);
            this.inventoryPanel.TabIndex = 6;
            this.inventoryPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPanel_Paint);
            // 
            // craftingPanel
            // 
            this.craftingPanel.Location = new System.Drawing.Point(176, 4);
            this.craftingPanel.Name = "craftingPanel";
            this.craftingPanel.Size = new System.Drawing.Size(96, 100);
            this.craftingPanel.TabIndex = 1;
            // 
            // inventoryText
            // 
            this.inventoryText.AutoSize = true;
            this.inventoryText.Location = new System.Drawing.Point(3, 0);
            this.inventoryText.Name = "inventoryText";
            this.inventoryText.Size = new System.Drawing.Size(35, 13);
            this.inventoryText.TabIndex = 0;
            this.inventoryText.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 745);
            this.Controls.Add(this.inventoryPanel);
            this.Controls.Add(this.mapPanel);
            this.Controls.Add(this.actionPanel);
            this.Controls.Add(this.listActions);
            this.Controls.Add(this.MessageOut);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mapPanel.ResumeLayout(false);
            this.mapPanel.PerformLayout();
            this.inventoryPanel.ResumeLayout(false);
            this.inventoryPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label MessageOut;
        private System.Windows.Forms.ListBox listActions;
        private System.Windows.Forms.FlowLayoutPanel actionPanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.Label mapText;
        private System.Windows.Forms.Panel inventoryPanel;
        private System.Windows.Forms.Label inventoryText;
        private System.Windows.Forms.Label roomView;
        private System.Windows.Forms.FlowLayoutPanel craftingPanel;
    }
}

