//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BodyBalance.Persistence
{
    using System;
    using System.Collections.Generic;
    
    public partial class HASINBASKET
    {
        public string USER_ID { get; set; }
        public string PRODUCT_ID { get; set; }
        public decimal QUANTITY { get; set; }
    
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual USER1 USER1 { get; set; }
    }
}
