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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.InitializeAsync();
        }
    }
}
