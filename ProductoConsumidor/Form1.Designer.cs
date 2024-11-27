namespace ProductoConsumidor
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
            BtnIniciar = new Button();
            TxtBoxBuffer = new TextBox();
            TxtBoxBufferList = new TextBox();
            SuspendLayout();
            // 
            // BtnIniciar
            // 
            BtnIniciar.Location = new Point(12, 12);
            BtnIniciar.Name = "BtnIniciar";
            BtnIniciar.Size = new Size(310, 34);
            BtnIniciar.TabIndex = 0;
            BtnIniciar.Text = "INICIAR";
            BtnIniciar.UseVisualStyleBackColor = true;
            BtnIniciar.Click += BtnIniciar_Click;
            // 
            // TxtBoxBuffer
            // 
            TxtBoxBuffer.Location = new Point(12, 52);
            TxtBoxBuffer.Multiline = true;
            TxtBoxBuffer.Name = "TxtBoxBuffer";
            TxtBoxBuffer.Size = new Size(310, 295);
            TxtBoxBuffer.TabIndex = 1;
            // 
            // TxtBoxBufferList
            // 
            TxtBoxBufferList.Location = new Point(12, 353);
            TxtBoxBufferList.Multiline = true;
            TxtBoxBufferList.Name = "TxtBoxBufferList";
            TxtBoxBufferList.Size = new Size(310, 171);
            TxtBoxBufferList.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 536);
            Controls.Add(TxtBoxBufferList);
            Controls.Add(TxtBoxBuffer);
            Controls.Add(BtnIniciar);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnIniciar;
        private TextBox TxtBoxBuffer;
        private TextBox TxtBoxBufferList;
    }
}
