namespace Core
{
    public interface IController : IInitialize
    {
        public void SetPause(bool value);
        public void Save();
    }
}