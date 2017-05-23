using System;
using DomainModel.DataContracts;

namespace DomainModel.DataModel
{
    public class Payment : IPayment
    {
        public double PaymentRate { get; set; }
        public DateTime EffectDateTime { get; set; }
    }
}