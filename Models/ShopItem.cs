namespace UbertweakNfcReaderWeb.Models;

public class ShopItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required ShopItemType Type { get; set; }
    public required int Price { get; set; }
    public Team? Owner { get; set; }
    public bool Available { get; set; } = false;
    public bool Redeemed { get; set; } = false;
    public Card? RewardCard { get; set; }
}

public enum ShopItemType
{
    StandardLego,
    SpecialLego,
    SpecialReward,
    Minifig
}