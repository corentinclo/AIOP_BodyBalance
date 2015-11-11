using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface IAccessoryServices
    {
        /// <summary>
        /// Create an accessory
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int CreateAccessory(AccessoryModel am);

        /// <summary>
        /// Find an accessory with its id
        /// </summary>
        /// <param name="AccessoryId"></param>
        /// <returns></returns>
        AccessoryModel FindAccessoryById(string AccessoryId);

        /// <summary>
        /// Update an accessory
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int UpdateAccessory(AccessoryModel am);

        /// <summary>
        /// Delete an accessory
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int DeleteAccessory(AccessoryModel am);

        /// <summary>
        /// Retrieve all the accessories
        /// </summary>
        /// <returns></returns>
        List<AccessoryModel> FindAllAccessories();
    }
}
