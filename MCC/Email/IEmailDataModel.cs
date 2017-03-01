namespace MCC.Email
{
    interface IEmailDataModel
    {
        object GetObjectById(string id, string language);
    }
}
