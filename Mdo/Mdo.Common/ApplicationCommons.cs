using System.Configuration;

namespace Mdo.Common
{
    public class ApplicationCommons
    {
        private ApplicationCommons()
        {

        }
        
        public static ApplicationCommons Instance { get; } = new ApplicationCommons();
    }
}
