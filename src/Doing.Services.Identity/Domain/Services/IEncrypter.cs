namespace Doing.Services.Identity.Domain.Services
{
    public interface IEncrypter
    {
        string GetSalt();

        string GetHash(string password, string saltedValue);


    }
}