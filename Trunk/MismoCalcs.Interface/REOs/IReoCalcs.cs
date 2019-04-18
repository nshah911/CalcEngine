using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.REOs
{
    public interface IReoCalcs
    {
        decimal PITIAforPrimaryResidence();

        decimal PITIAforSecondHome();

        decimal TotalNetRental();

        decimal SubTotalCurrentHousingPayment();

        decimal RealEstateRental();
    }
}
