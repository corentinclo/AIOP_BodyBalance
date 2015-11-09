using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface IEventServices
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
        /// <param name="id"></param>
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
        /// <param name="EventId"></param>
        /// <returns></returns>
        int DeleteEvent(EventModel em);

        /// <summary>
        /// Retrieve all events
        /// </summary>
        /// <returns></returns>
        List<EventModel> FindAllEvents();
    }
}
