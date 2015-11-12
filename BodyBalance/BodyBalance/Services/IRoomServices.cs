using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface IRoomServices
    {
        /// <summary>
        /// Create a room
        /// </summary>
        /// <param name="rm"></param>
        /// <returns></returns>
        int CreateRoom(RoomModel rm);

        /// <summary>
        /// Find a room with its id
        /// </summary>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        RoomModel FindRoomById(string RoomId);

        /// <summary>
        /// Update a room
        /// </summary>
        /// <param name="rm"></param>
        /// <returns></returns>
        int UpdateRoom(RoomModel rm);

        /// <summary>
        /// Delete a room
        /// </summary>
        /// <param name="rm"></param>
        /// <returns></returns>
        int DeleteRoom(RoomModel rm);

        /// <summary>
        /// Retrieve all the rooms
        /// </summary>
        /// <returns></returns>
        List<RoomModel> FindAllRooms();

        /// <summary>
        /// Find all the accessories of the room with the id in parameter
        /// </summary>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        List<AccessoryModel> FindAllAccessoriesOfRoom(string RoomId);

        /// <summary>
        /// Retrieve all the events which occured in the room with the id in parameter
        /// </summary>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        List<EventModel> FindAllEventsOfRoom(string RoomId);

        /// <summary>
        /// Add an accessory to the room with the id in parameter and set its quantity
        /// </summary>
        /// <param name="RoomId"></param>
        /// <param name="am"></param>
        /// <returns></returns>
        int AddAccessoryToRoom(string RoomId, AccessoryModel am, Nullable<decimal> quantity);

        /// <summary>
        /// Update the quantity of an accessory to the room with the id in parameter and its quantity
        /// </summary>
        /// <param name="RoomId"></param>
        /// <param name="am"></param>
        /// <returns></returns>
        int UpdateAccessoryInRoom(string RoomId, AccessoryModel am, Nullable<decimal> quantity);
    }
}
