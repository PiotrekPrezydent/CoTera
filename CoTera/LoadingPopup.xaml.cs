using CommunityToolkit.Maui.Views;

namespace CoTera;

public partial class LoadingPopup : Popup
{
	public LoadingPopup(string text)
	{
		InitializeComponent();
		LoadingText.Text = text;
	}

	public void SetText(string text) => LoadingText.Text = text;
}