namespace Examen1parcial
{
    public partial class App : Application
    {
        static Modelos.DBService dBServices;
        public static Modelos.DBService DataBase
        {
            get
            {
                if (dBServices == null)
                {
                    dBServices = new Modelos.DBService();
                }
                return dBServices;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.MainPage());
        }
    }
}
