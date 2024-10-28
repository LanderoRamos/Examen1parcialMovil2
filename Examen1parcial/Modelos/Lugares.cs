using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1parcial.Modelos
{
    [Table("Lugares")]
    public class Lugares
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }         // ID autoincrementable
        [SQLite.MaxLength(100)]
        public string Latitud { get; set; } = string.Empty;
        [SQLite.MaxLength(100)]
        public string Longitud { get; set; } = string.Empty;
        [SQLite.MaxLength(500)]
        public string Descripcion { get; set; } = string.Empty;
        public string FotoBase64 { get; set; } = string.Empty;   // Imagen en formato Base64
    }
}
