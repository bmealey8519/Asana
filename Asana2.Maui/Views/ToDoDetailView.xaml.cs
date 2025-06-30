using Asana2.Library.Models;
using Asana2.Maui.ViewModels;

namespace Asana2.Maui.Views;

[QueryProperty(nameof(ToDoId), "toDoId")]

public partial class ToDoDetailView : ContentPage
{
	public ToDoDetailView()
	{
		InitializeComponent();
		
	}

	public int ToDoId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

	private void OkClicked (object sender, EventArgs e)
	{
		(BindingContext as ToDoDetailViewModel)?.AddOrUpdateToDo();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
        BindingContext = new ToDoDetailViewModel(ToDoId);
    }
}