namespace GenericExtensions
{
    public static class PrimeTypeExtensions
    {
        public static int Direction(this float val)
        {
            return val == 0
                ? 0
                : val > 0
                    ? 1
                    : -1;
        }
    }
}
