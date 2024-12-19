namespace Books.Entity
{
    public enum BookSwapStatus
    {
        Available,      // Kitap takasa uygun
        PendingSwap,    // Kitap için takas teklifi var
        InSwapProcess,  // Kitap takas sürecinde
        NotAvailable    // Kitap takasa uygun değil
    }
}