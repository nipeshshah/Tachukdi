//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tachukdi.Models.DBM
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblPoint
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<int> Points { get; set; }
    }
}