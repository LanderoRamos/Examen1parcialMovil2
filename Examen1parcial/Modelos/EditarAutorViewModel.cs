using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1parcial.Modelos
{
    class EditarAutorViewModel
    {
        private DBService _dbService;
        public Modelos.Lugares Lugares { get; set; }

        public EditarAutorViewModel(Modelos.Lugares autor, DBService dbService)
        {
            Lugares = autor;
            _dbService = dbService;
            GuardarCommand = new Command(async () => await GuardarAutor());
        }

        public Command GuardarCommand { get; }

        private async Task GuardarAutor()
        {
            await _dbService.StoreLugares(Lugares); // Utiliza el método StoreAutor para actualizar o insertar
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
