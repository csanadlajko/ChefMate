namespace ChefMate_YR6LYT;

public partial class AddRecipePage : ContentPage
{
	private AddRecipePageViewModel viewModel;

    public AddRecipePage(AddRecipePageViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}