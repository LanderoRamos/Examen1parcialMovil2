namespace Examen1parcial.Views;

public partial class EditLugares : ContentPage
{
    private Modelos.DBService _dbService;
    public EditLugares(Modelos.Lugares lugares)
	{
		InitializeComponent();
        _dbService = new Modelos.DBService();
        BindingContext = lugares;
    }

    private async void Guardar_Clicked(object sender, EventArgs e)
    {
        // Llamar a DBService para guardar los cambios
        await _dbService.StoreLugares((Modelos.Lugares)BindingContext);
        await Navigation.PopAsync(); // Regresar a la página anterior
    }

}