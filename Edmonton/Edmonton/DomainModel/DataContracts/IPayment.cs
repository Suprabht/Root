using System;

namespace DomainModel.DataContracts
{
    public interface IPayment
    {
        double PaymentRate { get; set; }
        DateTime EffectDateTime { get; set; }
    }
}