namespace ChairShopping.ViewModel
{
    public class CouponViewModel
    {
        public Guid CouponCode { get; set; } = Guid.NewGuid();
        public DateTime ExpireDate { get; set; }
        public bool IsExpired { get; set; } = false;
    }
}
