using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface IManagerServices
    {
        /// <summary>
        /// Create a manager
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        int CreateManager(ManagerModel mm);

        /// <summary>
        /// Find a manager with his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ManagerModel FindManagerById(string id);

        /// <summary>
        /// Delete a manager
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        int DeleteManager(ManagerModel mm);

        /// <summary>
        /// Retrieve all the managers
        /// </summary>
        /// <returns></returns>
        List<ManagerModel> FindAllManagers();
    }
}
