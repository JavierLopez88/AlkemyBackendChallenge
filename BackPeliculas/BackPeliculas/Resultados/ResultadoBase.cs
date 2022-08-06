namespace BackPeliculas.Resultados;


public class ResultadoBase
{

    public bool Ok { get; set; } = true;
    public string Error { get; set; } = null!;
    public int StatusCode { get; set; }


    public void SetError(string error)
    {
        Ok = false;
        Error = error;
    }



}