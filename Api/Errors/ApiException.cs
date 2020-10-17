namespace Api.Errors
{
        // erreur 500
    // classe utilisée en mode developpement
    // on a les messages de ApiReponse plus le détail de l'érreur
    public class ApiException : ApiResponse
    {
        public ApiException(int errorNum, string message = null, string detail = null) : base(errorNum, message)
        {
            Detail = detail;
        }
        public string Detail { get; set; }
    }
}