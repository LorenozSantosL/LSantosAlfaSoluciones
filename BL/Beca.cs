using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Beca
    {
        public static ML.Result GetAllExcept(int IdBeca)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LSantosAlfaSolucionesEntities context = new DL.LSantosAlfaSolucionesEntities())
                {
                    var query = (from becaTDB in context.Beca
                                 where becaTDB.IdBeca != IdBeca
                                 select new
                                 {
                                     IdBeca = becaTDB.IdBeca,
                                     Nombre = becaTDB.Nombre
                                 }
                                 ).ToList();

                    result.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Beca beca = new ML.Beca();

                            beca.IdBeca = obj.IdBeca;
                            beca.Nombre = obj.Nombre;

                            result.Objects.Add(beca);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error; " + result.Ex;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LSantosAlfaSolucionesEntities context = new DL.LSantosAlfaSolucionesEntities())
                {
                    var query = (from becaTDB in context.Beca
                                 select new
                                 {
                                     IdBeca = becaTDB.IdBeca,
                                     Nombre = becaTDB.Nombre
                                 }
                                 ).ToList();

                    result.Objects = new List<object>();
                    if(query != null &&  query.ToList().Count > 0)
                    {
                        foreach(var obj in query)
                        {
                            ML.Beca beca = new ML.Beca();

                            beca.IdBeca = obj.IdBeca;
                            beca.Nombre = obj.Nombre;


                            result.Objects.Add(beca);
                        }
                        result.Correct = true ;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error; " + result.Ex;
            }

                return result;
        }
    }
}
