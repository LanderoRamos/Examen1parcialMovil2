using System.Threading;
using System.Threading.Tasks;


namespace Examen1parcial.Views;

public partial class Mapa : ContentPage
{
    private Modelos.DBService _dbService;
    public Mapa(Modelos.Lugares lugares)
	{
		InitializeComponent();
        BindingContext = lugares;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        SetUpMap(Lat.Text,Lon.Text);
    }


    private void SetUpMap(string latitude, string longitude)
    {
        var htmlSource = new HtmlWebViewSource
        {
            Html = $@"
                    <!DOCTYPE html>
                    <html lang='es'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Mapa de Google</title>
                    </head>
                    <body>
                        <center>
                        <iframe
                            src='https://maps.google.com/maps?q={latitude},{longitude}&hl=es;z=14&output=embed'
                            width='400'
                            height='600'
                            style='border:0;'
                            allowfullscreen=''
                            loading='lazy'
                            referrerpolicy='no-referrer-when-downgrade'
                        ></iframe></center>
                    </body>
                    </html>"
        };

        mapWebView.Source = htmlSource;
    }



}
