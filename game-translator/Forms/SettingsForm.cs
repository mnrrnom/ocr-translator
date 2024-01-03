using game_translator.Utils;

namespace game_translator.Forms;

public partial class SettingsForm : Form
{
    private AppConfiguration? _config;

    public SettingsForm()
    {
        InitializeComponent();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var config = AppConfigurationManager.GetConfiguration();
        if (config == null) return;

        config.GoogleTranslateToken = txtBearerToken.Text;
        config.GoogleTranslateApiKey = txtApiKey.Text;

        AppConfigurationManager.UpdateConfiguration(config);
        Close();
    }

    private void SettingsForm_Activated(object sender, EventArgs e)
    {
        _config = AppConfigurationManager.GetConfiguration();
        if (_config == null)
        {
            base.OnActivated(e);
            return;
        }

        txtBearerToken.Text = _config.GoogleTranslateToken;
        txtApiKey.Text = _config.GoogleTranslateApiKey;
    }
}