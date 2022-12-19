using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public bool Genero { get; set; }
        public int Edad { get; set; }

        public List<object> Alumnos { get; set; }

        public ML.Beca Beca { get; set; }

        public ML.AlumnoBeca AlumnoBeca { get; set; }
    }
}
