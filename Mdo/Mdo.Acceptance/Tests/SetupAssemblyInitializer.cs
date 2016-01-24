using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersistenceMocks;

namespace Mdo.Acceptance.Tests
{
    [TestClass]
    public class SetupAssemblyInitializer
    {
        public static UserWarehouse UserWarehouse;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            UserWarehouse = UserWarehouse.GetInstance();
        }
    }
}
