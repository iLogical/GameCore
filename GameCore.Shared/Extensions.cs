namespace GameCore
{
    public static class Extensions
    {
        public static bool IsNull(this object o)
        {
            return o == null;
        }
        public static bool IsNotNull(this object o)
        {
            return o != null;
        }
    }
}