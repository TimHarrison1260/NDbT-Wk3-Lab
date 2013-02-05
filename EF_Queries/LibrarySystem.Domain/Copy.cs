namespace LibrarySystem.Domain
{
    public class Copy
    {
        public virtual int CopyId { get; set; }
        public virtual bool IsAvailable { get; set; }
        public virtual Title Title { get; set; }
    }
}
