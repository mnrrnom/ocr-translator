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

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        rtbOutput = new RichTextBox();
        rtbRomaji = new RichTextBox();
        btnSettings = new Button();
        SuspendLayout();
        // 
        // rtbOutput
        // 
        rtbOutput.Dock = DockStyle.Fill;
        rtbOutput.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        rtbOutput.Location = new Point(10, 237);
        rtbOutput.Margin = new Padding(3, 10, 3, 3);
        rtbOutput.Name = "rtbOutput";
        rtbOutput.Size = new Size(684, 266);
        rtbOutput.TabIndex = 0;
        rtbOutput.Text = "Translation result";
        // 
        // rtbRomaji
        // 
        rtbRomaji.Dock = DockStyle.Top;
        rtbRomaji.Font = new Font("Segoe UI", 14F);
        rtbRomaji.Location = new Point(10, 10);
        rtbRomaji.Name = "rtbRomaji";
        rtbRomaji.Size = new Size(684, 227);
        rtbRomaji.TabIndex = 1;
        rtbRomaji.Text = "Selected text preview";
        // 
        // btnSettings
        // 
        btnSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSettings.Location = new Point(10, 509);
        btnSettings.Name = "btnSettings";
        btnSettings.Size = new Size(57, 31);
        btnSettings.TabIndex = 2;
        btnSettings.Text = "CFG";
        btnSettings.UseVisualStyleBackColor = true;
        btnSettings.Click += btnSettings_Click;
        // 
        // OutputForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(704, 553);
        Controls.Add(btnSettings);
        Controls.Add(rtbOutput);
        Controls.Add(rtbRomaji);
        Name = "OutputForm";
        Padding = new Padding(10, 10, 10, 50);
        Text = "OCR";
        ResumeLayout(false);
    }

    #endregion

    private RichTextBox rtbOutput;
    private RichTextBox rtbRomaji;
    private Button btnSettings;
}