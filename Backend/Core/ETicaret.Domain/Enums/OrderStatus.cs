using System.ComponentModel.DataAnnotations;

namespace ETicaret.Domain.Enums
{
    public enum  OrderStatus
    {
        [Display(Name = "Sipariş Oluşturuldu")]
        Created , 
        
        [Display(Name = "Yeni Sipariş")]
        NewOrder,

        [Display(Name = "Onaylandı Hazırlanıyor")]
        Approved,

        [Display(Name = "Sipariş Tamamlandı")]
        Completed
    }
}
