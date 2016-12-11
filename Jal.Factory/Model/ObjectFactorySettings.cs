using System;

namespace Jal.Factory.Model
{
    public static class ObjectFactorySettings
    {
        public static string DefaultNamePrefix = "Default";

        public static string DefaultNameSeparator = "_";

        public static string BuildDefaultName(Type target)
        {
            return DefaultNamePrefix + DefaultNameSeparator + target.Name;
        }
    }
}
