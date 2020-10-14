namespace Api.Errors
{
    public class ApiErrors
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiErrors(int errorNum, string message = null)
        {
            StatusCode = errorNum;
            //si messge = null on lance GetMessageErrorFromErrorNum 
            Message = message ?? GetMessageErrorFromErrorNum(StatusCode);
        }

        private string GetMessageErrorFromErrorNum(int num)
        {
            // ecriture de switch avec fonctions fléchées
            return num switch
            {
                400 => "Requête non valide, vous avez entré",
                401 => "Autorisé, vous n'êtes pas",
                404 => "Ressource, non trouvée",
                500 => "Erreur serveur, nous sommes désolés",
                _ => null // rien ne correspond
            };
        }
    }
}