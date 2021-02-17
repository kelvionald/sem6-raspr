namespace Valudator
{
    public interface IStorage
    {
        void Store(string key, string value);
        string Load(string key);
        bool IsExistsByValue(string value);
    }
}