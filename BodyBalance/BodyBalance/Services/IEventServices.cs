﻿using BodyBalance.Models;
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
        /// Retrieve all the users who subscribe to the event with the id in parameter
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        List<UserModel> FindUsersOfEvent(string EventId);

        /// <summary>
        /// Find the contributor of an event
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        UserModel FindContributorOfEvent(string EventId);

        /// <summary>
        /// Find the manager of an event
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        UserModel FindManagerOfEvent(string EventId);
    }
}
