using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface IContributorServices
    {
        /// <summary>
        /// Create a contributor
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        int CreateContributor(ContributorModel cm);

        /// <summary>
        /// Find a contributor with his id
        /// </summary>
        /// <param name="ContributorId"></param>
        /// <returns></returns>
        ContributorModel FindContributorById(string ContributorId);

        /// <summary>
        /// Update a contributor
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        int UpdateContributor(ContributorModel cm);

        /// <summary>
        /// Delete a contributor
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        int DeleteContributor(ContributorModel cm);

        /// <summary>
        /// Retrieve all the contributors
        /// </summary>
        /// <returns></returns>
        List<ContributorModel> FindAllContributors();

        /// <summary>
        /// Find all the events of a contributor with the id in parameter
        /// </summary>
        /// <param name="ContributorId"></param>
        /// <returns></returns>
        List<EventModel> FindAllEventsOfContributor(string ContributorId);
    }
}
