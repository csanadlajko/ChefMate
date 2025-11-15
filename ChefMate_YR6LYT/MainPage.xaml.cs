using CommunityToolkit.Mvvm.Messaging;

namespace ChefMate_YR6LYT
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = viewModel;

            WeakReferenceMessenger.Default.Register<string>(this, async (r, msg) =>
            {
                await DisplayAlert("Notification", msg, "OK");
            });
        }

        private async void OnMainPageLoaded(object? sender, EventArgs e)
        {
            await viewModel.InitializeAsync();
        }
    }
}
