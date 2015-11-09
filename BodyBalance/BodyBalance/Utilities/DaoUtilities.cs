using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Utilities
{
    public static class DaoUtilities
    {
        public const int SAVE_SUCCESSFUL = 1;
        public const int NO_CHANGES = 2;
        public const int UPDATE_EXCEPTION = 3; // An error occurred sending updates to the database.
        public const int UPDATE_CONCURRENCY_EXCEPTION = 4; //A database command did not affect the expected number of rows.This usually indicates an optimistic concurrency violation; that is, a row has been changed in the database since it was queried.
        public const int ENTITY_VALIDATION_EXCEPTION = 5; //The save was aborted because validation of entity property values failed.
        public const int UNSUPPORTED_EXCEPTION = 6; //An attempt was made to use unsupported behavior such as executing multiple asynchronous commands concurrently on the same context instance.
        public const int DISPOSED_EXCEPTION = 7; //The context or connection have been disposed.
        public const int INVALID_OPERATION_EXCEPTION = 8; //Some error occurred attempting to process entities in the context either before or after sending commands to the database.
    }
}