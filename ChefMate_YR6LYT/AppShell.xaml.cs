namespace ChefMate_YR6LYT
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("addrecipepage", typeof(AddRecipePage));
            Routing.RegisterRoute("editrecipepage", typeof(EditRecipePage));
        }
    }
}
