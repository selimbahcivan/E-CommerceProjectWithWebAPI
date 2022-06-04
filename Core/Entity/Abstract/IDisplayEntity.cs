namespace Core.Entity.Abstract
{
    public interface IDisplayEntity
    {
        public int DisplayOrder { get; set; }
        public bool IsDisplay { get; set; }
    }
}