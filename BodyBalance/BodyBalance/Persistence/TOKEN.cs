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
    
    public partial class TOKEN
    {
        public string USER_ID { get; set; }
        public string TOKEN1 { get; set; }
        public System.DateTime EXP_DATE { get; set; }
    
        public virtual USER1 USER1 { get; set; }
    }
}
