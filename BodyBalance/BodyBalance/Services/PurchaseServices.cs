using BodyBalance.Persistence;
using BodyBalance.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyBalance.Models;

namespace BodyBalance.Services
{
    public class PurchaseServices : IPurchaseServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();

        public int CreatePurchase(PurchaseModel pm)
        {
            throw new NotImplementedException();
        }

        public PurchaseModel FindPurchaseWithId(string PurchaseId)
        {
            throw new NotImplementedException();
        }

        public int UpdatePurchase(PurchaseModel pm)
        {
            throw new NotImplementedException();
        }

        public int DeletePurchase(PurchaseModel pm)
        {
            throw new NotImplementedException();
        }

        public List<PurchaseModel> FindAllPurchases()
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> FindAllProductsOfPurchase(string PurchaseId)
        {
            throw new NotImplementedException();
        }
    }
}