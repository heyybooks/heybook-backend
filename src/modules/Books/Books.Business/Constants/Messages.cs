namespace Books.Business.Constants
{
    public static class Messages
    {
        public static string BookAdded = "Kitap eklendi.";
        public static string BookNotFound = "Kitap bulunamadı.";
        public static string BookUpdated = "Kitap güncellendi.";
        public static string BookListed = "Kitap listelendi.";
        public static string BookDeleted = "Kitap silindi.";
        public static string BookInvalid = "Geçersiz kitap.";

        public static string MapperNotNull = "Mapper null olamaz.";
        public static string MapperSuccessfully = "Mapper başarıyla yapılandırıldı.";

        public static string BookCreateDtoNull = "BookCreateDto null olamaz.";
        public static string BookCreateDtoCreated = "BookCreateDto, Book ile başarıyla oluşturuldu.";

        public static string MapperNotConfigured = "Mapper yapılandırılmamış. Lütfen kullanmadan önce yapılandırın.";
        public static string BookCreateDtoNoImages = "BookCreateDto içinde resim bulunamadı.";
        public static string BookImagesCreated = "Kitap resimleri DTO'dan başarıyla oluşturuldu.";
    }
}
