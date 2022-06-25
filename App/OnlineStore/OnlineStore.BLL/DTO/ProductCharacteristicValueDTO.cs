namespace OnlineStore.BLL.DTO
{
    public partial class ProductCharacteristicValueDTO
    {
        public int ProductId { get; set; }
        public int CharacteristicValueId { get; set; }

        public virtual CharacteristicValueDTO CharacteristicValue { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
