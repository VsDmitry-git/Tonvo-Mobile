using Tonvo.Services;

namespace Tonvo_Mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("RootView", typeof(RootView));
        Routing.RegisterRoute("ApplicantList", typeof(ApplicantList));
        Routing.RegisterRoute("UserInfoView", typeof(UserInfoView));
        Routing.RegisterRoute("AccountView", typeof(AccountView));
        Routing.RegisterRoute("ApplicantInfoView", typeof(ApplicantInfoView));
    }
}
