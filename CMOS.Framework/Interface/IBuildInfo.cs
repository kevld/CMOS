namespace CMOS.Framework.Interface
{
    public interface IBuildInfo
    {
        public int Major { get; }
        public int Minor { get; }
        public int Build { get; }

        public string ToString();
    }
}
