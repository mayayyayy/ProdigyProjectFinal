using ProdigyProjectFinal.View;
using ProdigyProjectFinal.ViewModel;

namespace ProdigyProjectFinal;

public partial class App : Application
{
	private bool showFlyout;

    private AppShellViewModel shellVM;
	public bool ShowFlyouts { get => shellVM.Visible; set { shellVM.Visible = value; } }
    public bool ShowFlyouts2 { get { if (showFlyout) return false; return true; } set { shellVM.AntiVisible = value; } }

    public App()
	{

		
        InitializeComponent();
		//	shellVM = vm;
		MainPage = new AppShell();//vm);
	
		
	}
}
