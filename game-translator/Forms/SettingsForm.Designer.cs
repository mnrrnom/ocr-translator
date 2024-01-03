using System.ComponentModel;

namespace game_translator.Forms;

partial class SettingsForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        components = new Container();
        label1 = new Label();
        label2 = new Label();
        txtBearerToken = new TextBox();
        txtApiKey = new TextBox();
        btnSave = new Button();
        toolTipBearer = new ToolTip(components);
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(14, 10);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(218, 21);
        label1.TabIndex = 0;
        label1.Text = "Google Translate Bearer token";
        toolTipBearer.SetToolTip(label1, "Set up google cloud cli and get bearer token using:\r\ngcloud auth print-access-token\r\n");
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(11, 90);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(182, 21);
        label2.TabIndex = 1;
        label2.Text = "Google Translate API Key";
        // 
        // txtBearerToken
        // 
        txtBearerToken.Location = new Point(14, 34);
        txtBearerToken.Name = "txtBearerToken";
        txtBearerToken.Size = new Size(557, 29);
        txtBearerToken.TabIndex = 2;
        // 
        // txtApiKey
        // 
        txtApiKey.Location = new Point(11, 114);
        txtApiKey.Name = "txtApiKey";
        txtApiKey.Size = new Size(560, 29);
        txtApiKey.TabIndex = 3;
        // 
        // btnSave
        // 
        btnSave.Dock = DockStyle.Bottom;
        btnSave.Location = new Point(10, 398);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(564, 35);
        btnSave.TabIndex = 4;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        // 
        // SettingsForm
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(584, 443);
        Controls.Add(btnSave);
        Controls.Add(txtApiKey);
        Controls.Add(txtBearerToken);
        Controls.Add(label2);
        Controls.Add(label1);
        Font = new Font("Segoe UI", 12F);
        Margin = new Padding(4);
        Name = "SettingsForm";
        Padding = new Padding(10);
        Text = "Settings";
        Activated += SettingsForm_Activated;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
    private TextBox txtBearerToken;
    private TextBox txtApiKey;
    private Button btnSave;
    private ToolTip toolTipBearer;
}