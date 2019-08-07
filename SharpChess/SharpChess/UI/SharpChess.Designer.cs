using SharpChess;
using SharpChess.UI;

namespace SharpChess
{
    partial class SharpChess
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
            this.tileCoordinate_lbl = new System.Windows.Forms.Label();
            this.currentPiece_lbl = new System.Windows.Forms.Label();
            this.newGame_btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.moveHistory_txtBox = new System.Windows.Forms.RichTextBox();
            this.moveHistory_lbl = new System.Windows.Forms.Label();
            this.turnbased_radioBtn = new System.Windows.Forms.RadioButton();
            this.freePlay_radioBtn = new System.Windows.Forms.RadioButton();
            this.boardPanel = new UI.DoubleBuffer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.blackteam_lbl = new System.Windows.Forms.Label();
            this.whiteteam_lbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tileCoordinate_lbl
            // 
            this.tileCoordinate_lbl.AutoSize = true;
            this.tileCoordinate_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileCoordinate_lbl.ForeColor = System.Drawing.SystemColors.Control;
            this.tileCoordinate_lbl.Location = new System.Drawing.Point(672, 733);
            this.tileCoordinate_lbl.Name = "tileCoordinate_lbl";
            this.tileCoordinate_lbl.Size = new System.Drawing.Size(118, 21);
            this.tileCoordinate_lbl.TabIndex = 1;
            this.tileCoordinate_lbl.Text = "Tile Coordinate:";
            // 
            // currentPiece_lbl
            // 
            this.currentPiece_lbl.AutoSize = true;
            this.currentPiece_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentPiece_lbl.ForeColor = System.Drawing.SystemColors.Control;
            this.currentPiece_lbl.Location = new System.Drawing.Point(672, 767);
            this.currentPiece_lbl.Name = "currentPiece_lbl";
            this.currentPiece_lbl.Size = new System.Drawing.Size(49, 21);
            this.currentPiece_lbl.TabIndex = 2;
            this.currentPiece_lbl.Text = "Piece:";
            // 
            // newGame_btn
            // 
            this.newGame_btn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.newGame_btn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newGame_btn.ForeColor = System.Drawing.SystemColors.Control;
            this.newGame_btn.Location = new System.Drawing.Point(12, 48);
            this.newGame_btn.Name = "newGame_btn";
            this.newGame_btn.Size = new System.Drawing.Size(150, 55);
            this.newGame_btn.TabIndex = 3;
            this.newGame_btn.Text = "New Game";
            this.newGame_btn.UseVisualStyleBackColor = false;
            this.newGame_btn.Click += new System.EventHandler(this.newGame_btn_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(745, 668);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 55);
            this.button1.TabIndex = 4;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // moveHistory_txtBox
            // 
            this.moveHistory_txtBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.moveHistory_txtBox.Location = new System.Drawing.Point(745, 36);
            this.moveHistory_txtBox.Name = "moveHistory_txtBox";
            this.moveHistory_txtBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.moveHistory_txtBox.Size = new System.Drawing.Size(224, 616);
            this.moveHistory_txtBox.TabIndex = 5;
            this.moveHistory_txtBox.Text = "";
            // 
            // moveHistory_lbl
            // 
            this.moveHistory_lbl.AutoSize = true;
            this.moveHistory_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveHistory_lbl.ForeColor = System.Drawing.SystemColors.Control;
            this.moveHistory_lbl.Location = new System.Drawing.Point(741, 9);
            this.moveHistory_lbl.Name = "moveHistory_lbl";
            this.moveHistory_lbl.Size = new System.Drawing.Size(106, 21);
            this.moveHistory_lbl.TabIndex = 6;
            this.moveHistory_lbl.Text = "Move History:";
            // 
            // turnbased_radioBtn
            // 
            this.turnbased_radioBtn.AutoSize = true;
            this.turnbased_radioBtn.BackColor = System.Drawing.Color.Transparent;
            this.turnbased_radioBtn.Checked = true;
            this.turnbased_radioBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnbased_radioBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.turnbased_radioBtn.Location = new System.Drawing.Point(514, 32);
            this.turnbased_radioBtn.Name = "turnbased_radioBtn";
            this.turnbased_radioBtn.Size = new System.Drawing.Size(107, 25);
            this.turnbased_radioBtn.TabIndex = 7;
            this.turnbased_radioBtn.TabStop = true;
            this.turnbased_radioBtn.Text = "Turn-Based";
            this.turnbased_radioBtn.UseVisualStyleBackColor = false;
            this.turnbased_radioBtn.CheckedChanged += new System.EventHandler(this.turnbased_radioBtn_CheckedChanged);
            // 
            // freePlay_radioBtn
            // 
            this.freePlay_radioBtn.AutoSize = true;
            this.freePlay_radioBtn.BackColor = System.Drawing.Color.Transparent;
            this.freePlay_radioBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freePlay_radioBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.freePlay_radioBtn.Location = new System.Drawing.Point(514, 65);
            this.freePlay_radioBtn.Name = "freePlay_radioBtn";
            this.freePlay_radioBtn.Size = new System.Drawing.Size(91, 25);
            this.freePlay_radioBtn.TabIndex = 8;
            this.freePlay_radioBtn.TabStop = true;
            this.freePlay_radioBtn.Text = "Free Play";
            this.freePlay_radioBtn.UseVisualStyleBackColor = false;
            this.freePlay_radioBtn.CheckedChanged += new System.EventHandler(this.freePlay_radioBtn_CheckedChanged);
            // 
            // boardPanel
            // 
            this.boardPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.boardPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.boardPanel.Location = new System.Drawing.Point(12, 12);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(640, 640);
            this.boardPanel.TabIndex = 0;
            this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPanel_Paint);
            this.boardPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.boardPanel_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.newGame_btn);
            this.panel1.Controls.Add(this.freePlay_radioBtn);
            this.panel1.Controls.Add(this.turnbased_radioBtn);
            this.panel1.Location = new System.Drawing.Point(12, 685);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 114);
            this.panel1.TabIndex = 9;
            // 
            // blackteam_lbl
            // 
            this.blackteam_lbl.AutoSize = true;
            this.blackteam_lbl.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackteam_lbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.blackteam_lbl.Location = new System.Drawing.Point(658, 72);
            this.blackteam_lbl.Name = "blackteam_lbl";
            this.blackteam_lbl.Size = new System.Drawing.Size(79, 30);
            this.blackteam_lbl.TabIndex = 10;
            this.blackteam_lbl.Text = "BLACK";
            // 
            // whiteteam_lbl
            // 
            this.whiteteam_lbl.AutoSize = true;
            this.whiteteam_lbl.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteteam_lbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.whiteteam_lbl.Location = new System.Drawing.Point(658, 557);
            this.whiteteam_lbl.Name = "whiteteam_lbl";
            this.whiteteam_lbl.Size = new System.Drawing.Size(80, 30);
            this.whiteteam_lbl.TabIndex = 11;
            this.whiteteam_lbl.Text = "WHITE";
            // 
            // SharpChess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(984, 811);
            this.Controls.Add(this.whiteteam_lbl);
            this.Controls.Add(this.blackteam_lbl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.moveHistory_lbl);
            this.Controls.Add(this.tileCoordinate_lbl);
            this.Controls.Add(this.currentPiece_lbl);
            this.Controls.Add(this.moveHistory_txtBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.boardPanel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximumSize = new System.Drawing.Size(1000, 850);
            this.MinimumSize = new System.Drawing.Size(1000, 850);
            this.Name = "SharpChess";
            this.Text = "SharpChess";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBuffer boardPanel;
        private System.Windows.Forms.Label tileCoordinate_lbl;
        private System.Windows.Forms.Label currentPiece_lbl;
        private System.Windows.Forms.Button newGame_btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox moveHistory_txtBox;
        private System.Windows.Forms.Label moveHistory_lbl;
        private System.Windows.Forms.RadioButton turnbased_radioBtn;
        private System.Windows.Forms.RadioButton freePlay_radioBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label blackteam_lbl;
        private System.Windows.Forms.Label whiteteam_lbl;
    }
}

