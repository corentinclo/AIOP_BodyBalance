using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface IActivityServices
    {
        /// <summary>
        /// Create an activity
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int CreateActivity(ActivityModel am);

        /// <summary>
        /// Find an activity with his id
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <returns></returns>
        ActivityModel FindActivityById(string ActivityId);

        /// <summary>
        /// Update an activity
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int UpdateActivity(ActivityModel am);

        /// <summary>
        /// Delete an activity
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int DeleteActivity(ActivityModel am);

        /// <summary>
        /// Retrieve all the activities
        /// </summary>
        /// <returns></returns>
        List<ActivityModel> FindAllActivities();

        /// <summary>
        /// Find all the activities of a manager with the id in parameter
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <returns></returns>
        List<ActivityModel> FindAllActivityOfManager(string ManagerId);
    }
}
