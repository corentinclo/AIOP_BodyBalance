using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface IRoomServices
    {
        int CreateRoom(RoomModel rm);
        RoomModel FindRoomById(string RoomId);
        int UpdateRoom(RoomModel rm);
        int DeleteRoom(RoomModel rm);
        List<RoomModel> FindAllRooms();
        List<AccessoryModel> FindAllAccessoriesOfRoom(string RoomId);
        List<EventModel> FindAllEventsOfRoom(string RoomId);

    }
}
