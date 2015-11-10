using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface IAccessoryServices
    {
        int CreateAccessory(AccessoryModel am);
        AccessoryModel FindAccessoryById(string AccessoryId);
        int UpdateAccessory(AccessoryModel am);
        int DeleteAccessory(AccessoryModel am);
        List<AccessoryModel> FindAllAccessories();
    }
}
