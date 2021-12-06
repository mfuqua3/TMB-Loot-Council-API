namespace LootCouncil.Domain
{
    public static class DataConstants
    {
        public static class ItemFilters
        {
            public const string Offspec = "Offspec";
            public const string NumberPlaceholder = ":num";
            public const string WishlistNumber = "Wishlist " + NumberPlaceholder;
            public const string TbcPhaseNumber = "TBC Phase " + NumberPlaceholder;
        }

        public static class ItemTypes
        {
            public const string Wishlist = "Wishlist";
            public const string Received = "Recevied";
            public const string Priority = "Priority";
        }
    }
}