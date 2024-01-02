namespace game_translator.Forms;

partial class OutputForm
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

    private RichTextBox rtbOutput;
    private RichTextBox rtbRomaji;
    
    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "";
        this.rtbRomaji = new RichTextBox() { Location = new(10, 10), Size = new (780, 230), ReadOnly = true };
        this.rtbOutput = new RichTextBox() { Location = new(10, 240), Size = new (780, 200), ReadOnly = true };
        Controls.Add(rtbOutput);
        Controls.Add(rtbRomaji);
    }

    #endregion
}