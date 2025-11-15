namespace ChefMate_YR6LYT;

public partial class EditRecipePage : ContentPage
{
	private EditRecipePageViewModel viewModel;
	
	public EditRecipePage(EditRecipePageViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        viewModel.InitDraft();
    }
}