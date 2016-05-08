using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess;

namespace BusinessLogic
{
    public class ExEventManager
    {
        public ExEvent FetchEvent(int eventID)
        {
            try
            {
                return ExEventAccessor.SelectSingleEvent(eventID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ExEvent> FetchEventsByAgent(int employeeID, string statusName, DateTime startDate, DateTime endDate)
        {
            try
            {
                return ExEventAccessor.SelectEventsByAgent(employeeID, statusName, startDate, endDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddExEvent(ExEvent newEvent)
        {
            int rows;
            try
            {
                rows = ExEventAccessor.InsertExEvent(newEvent);
            }
            catch (Exception)
            {
                throw;
            }

            if (rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateExEvent(ExEvent oldE, ExEvent newE)
        {
            try
            {
                ExEventAccessor.UpdateSubmission(oldE, newE);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateExEventToPending(ExEvent oldE, ExEvent newE)
        {
            try
            {
                ExEventAccessor.UpdateSubmissionToPending(oldE, newE);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteExEvent(ExEvent oldE)
        {
            try
            {
                if (ExEventAccessor.DeleteExEvent(oldE) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }
        }

        public List<Activity> FetchActivityList()
        {
            try
            {
                return ExEventAccessor.SelectActivityList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ExEvent> FetchCompletedList()
        {
            try
            {
                return ExEventAccessor.SelectCompletedEvents();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Status> FetchStatusList()
        {
            try
            {
                return ExEventAccessor.SelectStatusList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
