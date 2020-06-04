//-----------------------------------------------------------------------------------------
// Developer    :  ASHISH GUPTA
// File Name    :  Customer.cs
// Create Date  :  <17th May,2020>
// Last Updated :  <20th May,2020>
// Description  :  customer Model with all the properties
// ------------------------------------------------------------------------------------------


export class Customer
{
    constructor(
        public Name:string = '',
        public Email:string = '',
        public Pwd:string = '',
        public DOB:Date = null,
        public WalletBalance:number = null,
        public ResidingAddress :string = '',
        public PhoneNo :number = null
    ){}

    
}

