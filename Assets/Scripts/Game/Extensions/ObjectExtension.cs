namespace Game.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object value)
        {
            return value == null || value.Equals(null);
        }
        
        public static bool IsNotNull(this object value)
        {
            return !IsNull(value);
        }
        
        
    }
}