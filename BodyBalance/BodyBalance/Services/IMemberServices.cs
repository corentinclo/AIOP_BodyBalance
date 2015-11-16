using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface IMemberServices
    {
        /// <summary>
        /// Create a member
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        int CreateMember(MemberModel mm);

        /// <summary>
        /// Find a member with his id
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        MemberModel FindMemberById(string MemberId);

        /// <summary>
        /// Update a member
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        int UpdateMember(MemberModel mm);

        /// <summary>
        /// Delete a member
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        int DeleteMember(MemberModel mm);

        /// <summary>
        /// Retrieve all the members
        /// </summary>
        /// <returns></returns>
        List<MemberModel> FindAllMembers();


    }
}
