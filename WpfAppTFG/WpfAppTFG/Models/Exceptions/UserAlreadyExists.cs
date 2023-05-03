using System;

namespace WpfAppTFG.Model.Exception
{
    [Serializable]
    public class UserAlreadyExists : System.Exception
    {
        public UserAlreadyExists()
        {
        }

        public UserAlreadyExists(string? message) : base(message)
        {
        }

        public UserAlreadyExists(string? message, System.Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
