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
        /// <param name="em"></param>
        /// <returns></returns>
        int CreateEvent(EventModel em);

        /// <summary>
        /// Create a PunctualEvent
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int CreatePunctualEvent(PunctualEventModel em);

        /// <summary>
        /// Create a ReptitiveEvent
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int CreateRepetitiveEvent(RepetitiveEventModel em);

        /// <summary>
        /// Add an occurence of an Event to ReptitiveEvent
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int AddRepetitiveEventOccurrence(RepetitiveEventModel em);

        /// <summary>
        /// Find an Event with its id
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        EventModel FindEventById(string EventId);

        /// <summary>
        /// Find a PunctualEvent with its id
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        PunctualEventModel FindPunctualEventById(string EventId);

        /// <summary>
        /// Find a RepetitiveEventList with its id 
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        List<RepetitiveEventModel> FindRepetitiveEventsById(string EventId);

        /// <summary>
        /// Find a RepetitiveEvent with its id and date
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        RepetitiveEventModel FindRepetitiveEventByIdAndDate(string EventId, System.DateTime EventDate);

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int UpdateEvent(EventModel em);

        /// <summary>
        /// Update a Punctualevent
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int UpdatePunctualEvent(PunctualEventModel em);

        /// <summary>
        /// Update a Repetitiveevent
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int UpdateRepetitiveEvent(RepetitiveEventModel em);

        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int DeleteEvent(EventModel em);

        /// <summary>
        /// Delete a punctualevent
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int DeletePunctualEvent(PunctualEventModel em);

        /// <summary>
        /// Delete a repetitiveevent
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        int DeleteRepetitiveEvent(RepetitiveEventModel em);

        /// <summary>
        /// Delete totaly a RepetitveEvent i.e. all of its occurence
        /// </summary>r
        /// <param name="em"></param>
        /// <returns></returns>
        int DeleteRepetitiveEvents(EventModel em);

        /// <summary>
        /// Retrieve all events
        /// </summary>
        /// <returns></returns>
        List<EventModel> FindAllEvents();

        /// <summary>
        /// Retrieve all the punctual events
        /// </summary>
        /// <returns></returns>
        List<PunctualEventModel> FindAllPunctualEvents();

        /// <summary>
        /// Retrieve all the repetitive events
        /// </summary>
        /// <returns></returns>
        List<RepetitiveEventModel> FindAllRepetitiveEvents();

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
        ContributorModel FindContributorOfEvent(string EventId);

        /// <summary>
        /// Find the manager of an event
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        ManagerModel FindManagerOfEvent(string EventId);

        /// <summary>
        /// Register the user in parameter to the event with the id in parameter
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="um"></param>
        /// <returns></returns>
        int RegisterUserToEvent(string EventId, UserModel um);

        /// <summary>
        /// Remove the user in parameter of the event with the id in parameter
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="um"></param>
        /// <returns></returns>
        int RemoveUserOfEvent(string EventId, UserModel um);
    }
}
