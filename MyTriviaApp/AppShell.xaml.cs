using MyTriviaApp.Views;

namespace MyTriviaApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        private void RoutingPages()
        {
            Routing.RegisterRoute("BestScores", typeof(BestScoresPage));
            Routing.RegisterRoute("Login", typeof(LoginPage));

        }
    }
}