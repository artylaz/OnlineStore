namespace OnlineStore.WEB.Models
{
    public partial class ProductCharacteristicValueVM
    {
        public int ProductId { get; set; }
        public int CharacteristicValueId { get; set; }

        public virtual CharacteristicValueVM CharacteristicValue { get; set; }
        public virtual ProductVM Product { get; set; }
    }
}
