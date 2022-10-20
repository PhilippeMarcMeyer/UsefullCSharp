        [WebMethod]
        public static Dictionary<string, int> GetReservationStatus()
        {
            return Enum.GetValues(typeof(ReservationStatus))
               .Cast<ReservationStatus>()
               .ToDictionary(t => t.ToString(), t => (int)t);
        }

        [WebMethod]
        public static Dictionary<string,int> GetCancelBy()
        {
            return Enum.GetValues(typeof(ReservationCancelBy))
               .Cast<ReservationCancelBy>()
               .ToDictionary(t => t.ToString(), t => (int)t);
        }
