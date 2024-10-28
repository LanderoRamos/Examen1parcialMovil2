namespace Examen1parcial.Views;

public partial class ListaLugares : ContentPage
{
    private Modelos.DBService _dbService;

    public ListaLugares()
	{
		InitializeComponent();
        _dbService = new Modelos.DBService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Obtener la lista de personas de la base de datos
        var LugarList = await _dbService.GetLugares();
        // Asignar la lista al ListView
        LugarListView.ItemsSource = LugarList;
      
    }

    private async void LugarListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {

        string action = await DisplayActionSheet("Selecciona una opci�n", "Cancelar", null, "Mapa","Editar","Compartir");

        if (action != null)
        {
            // Realiza una acci�n basada en la opci�n seleccionada
            switch (action)
            {
                case "Mapa":

                    //await DisplayAlert("Seleccionaste", "Opci�n 1", "OK");
                     if (e.Item == null) return;

                    var autorSeleccionado = (Modelos.Lugares)e.Item;
                    await Navigation.PushAsync(new Mapa(autorSeleccionado));

                    ((ListView)sender).SelectedItem = null;

                    break;
                case "Editar":

                    if (e.Item == null) return;

                    var Seleccionado = (Modelos.Lugares)e.Item;
                    await Navigation.PushAsync(new EditLugares(Seleccionado));

                    ((ListView)sender).SelectedItem = null;

                    break;

                case "Compartir":

                    if (e.Item == null) return;
                   
                    var Selecciona = (Modelos.Lugares)e.Item;
                    
                    string currentLatitude = Selecciona.Latitud;
                    string currentLongitude = Selecciona.Longitud;
                    string descripcion = Selecciona.Descripcion;

                    try
                    {
                        var locationText = $"https://maps.google.com/?q={currentLatitude},{currentLongitude}";
                        await Share.RequestAsync(new ShareTextRequest
                        {
                            Text = $"Mi ubicaci�n actual es: {locationText} \n {descripcion}",
                            Title = "Compartir Ubicaci�n"
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }


                            break;

                default:
                    await DisplayAlert("Cancelar", "No seleccionaste ninguna opci�n.", "OK");
                    break;
            }
        }
    }

    private async void EliminarAutor_SwipeItemInvoked(object sender, EventArgs e)
    {
        var swipeItem = (SwipeItem)sender;
        var autor = (Modelos.Lugares)swipeItem.CommandParameter;

        bool confirmacion = await DisplayAlert("Eliminar", "�Est�s seguro de eliminar este autor?", "S�", "No");
        if (confirmacion)
        {
            await _dbService.DeleteLugares(autor); // Eliminar de la base de datos
            LugarListView.ItemsSource = await _dbService.GetLugares(); // Recargar la lista
        }
    }

}