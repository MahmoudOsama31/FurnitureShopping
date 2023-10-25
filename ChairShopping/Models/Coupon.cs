namespace ChairShopping.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public Guid CouponCode { get; set; } = new Guid();
        public DateTime ExpireDate { get; set; }
        public bool IsExpired { get; set; }
    }
}
