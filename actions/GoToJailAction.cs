using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents the "go to jail" action and stores the jail board space ID.
    /// </summary>
    public class GoToJailAction : IAction
    {
        int jailSpaceID;

        /// <summary>
        /// Constructor for a "go to jail" action.
        /// </summary>
        /// <param name="jailSpaceID">Jail board space number that the player must move to.</param>
        public GoToJailAction(int jailSpaceID)
        {
            this.jailSpaceID = jailSpaceID;
        }

        /// <summary>
        /// Return board space number of the jail space the player must move to.
        /// </summary>
        /// <returns>Jail space ID</returns>
        public int GetJailSpaceID()
        {
            return this.jailSpaceID;
        }

    }
}
