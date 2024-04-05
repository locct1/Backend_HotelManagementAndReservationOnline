using MyApiNetCore6.Models.VnPay;

namespace MyApiNetCore6.Repositories.ClientRepo
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
