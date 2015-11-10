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
        /// <param name="ManagerId"></param>
        /// <returns></returns>
        ManagerModel FindManagerById(string ManagerId);

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

        /// <summary>
        /// Find all the activities of a manager with the id in parameter
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <returns></returns>
        List<ActivityModel> FindAllActivitiesOfManager(string ManagerId);

        /// <summary>
        /// Find all the events of a manager with the id in parameter
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <returns></returns>
        List<EventModel> FindAllEventsOfManager(string ManagerId);
    }
}
