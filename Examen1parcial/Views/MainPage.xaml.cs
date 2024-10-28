//using Plugin.Geolocator;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Examen1parcial.Views;

public partial class MainPage : ContentPage
{

    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;
    string base64Image;

    public MainPage()
	{
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetCurrentLocation();
    }

    public async void btnagregar_Clicked(object sender, EventArgs e)
    {
        string lat = txtlatitud.Text;
        string lon = txtlongitud.Text;
        string des = descripcion.Text;

        if (string.IsNullOrWhiteSpace(des) || base64Image == null)
        {
            DisplayAlert("Aviso", "Llene todos los datos para poder ingresar", "OK");
            //return; // No continuar si el campo está vacío
        }
        else
        {
           // DisplayAlert("Exito", "Datos Agregados correctamente", "OK");
            var lugares = new Modelos.Lugares
            {
                Latitud = lat,
                Longitud = lon,
                Descripcion = des,
                FotoBase64 = base64Image
            };


            if (await App.DataBase.StoreLugares(lugares) > 0)
            {
                await DisplayAlert("Aviso", "Registrado con exito", "OK");
                limpiar();


            }
        }
    }

    private void btnsalir_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnlista_Clicked(object sender, EventArgs e)
    {
        var pagina = new Views.ListaLugares();
        await Navigation.PushAsync(pagina);
    }

    private void btnimagen_Clicked(object sender, EventArgs e)
    {
        TakePhoto();
    }  

    public async Task GetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            txtlatitud.Text = "" + location.Latitude;
            txtlongitud.Text = ""+ location.Longitude;
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
        }
        finally
        {
            _isCheckingLocation = false;
        }
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }

    // Tomar foto inicio
    public async void TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // Guardar el archivo en almacenamiento local
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                // Usar un bloque using para asegurarse de que los streams se cierran correctamente
                using (Stream sourceStream = await photo.OpenReadAsync())
                {
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                    }
                }

                // Mostrar la imagen capturada
                capturedImage.Source = ImageSource.FromFile(localFilePath);

                // Convertir la imagen a base64 (asegurarse de que los flujos anteriores están cerrados)
                base64Image = await ConvertImageToBase64(localFilePath);
                //await DisplayAlert("Imagen en Base64", base64Image, "OK");
            }
        }
    }
    //fin de tomar foto

    private async Task<string> ConvertImageToBase64(string imagePath)
    {
        byte[] imageBytes;
        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await fs.CopyToAsync(ms);
                imageBytes = ms.ToArray();
            }
        }

        // Convertir los bytes de la imagen a una cadena base64
        return Convert.ToBase64String(imageBytes);
    }

    void limpiar()
    {
        GetCurrentLocation();
        descripcion.Text = "";
   
    }
}