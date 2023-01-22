namespace Core
{
    public interface IController : IInitialize
    {
        public void Pause(bool pause);
    }
}