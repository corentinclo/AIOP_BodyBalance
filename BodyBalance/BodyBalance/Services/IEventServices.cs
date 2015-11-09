using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface IEventServices
    {
        /// <summary>
        /// Create an event
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int CreateEvent(EventModel em);

        /// <summary>
        /// Find an Event with his id
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        EventModel FindEventById(string EventId);

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int UpdateEvent(EventModel em);

        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int DeleteEvent(EventModel em);

        /// <summary>
        /// Retrieve all events
        /// </summary>
        /// <returns></returns>
        List<EventModel> FindAllEvents();

        /// <summary>
        /// Retrieve all event of the activity with the id in parameter
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <returns></returns>
        List<EventModel> FindAllEventsOfActivity(string ActivityId);

        /// <summary>
        /// Find all the events of a contributor with the id in parameter
        /// </summary>
        /// <param name="ContributorId"></param>
        /// <returns></returns>
        List<EventModel> FindAllEventsOfContributor(string ContributorId);

        /// <summary>
        /// Find all the events of a manager with the id in parameter
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <returns></returns>
        List<EventModel> FindAllEventsOfManager(string ManagerId);
    }
}
