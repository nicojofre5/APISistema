namespace APIsistemaGestion.Excepciones
{
    public class Excepciongral : Exception
    {
       

            public int HttpStatusCode { get; }

            public Excepciongral(string message, int httpStatusCode) : base(message)
            {
                HttpStatusCode = httpStatusCode;
            }
        }
    }

