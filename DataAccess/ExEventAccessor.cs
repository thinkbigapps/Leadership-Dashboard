using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class ExEventAccessor
    {
        public static List<ManagerExEvent> SelectManagerEvents(string dept)
        {
            var myList = new List<ManagerExEvent>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = "SELECT event_id, ex_event.employee_id, employee.department_name, employee.employee_id, event_date, submission_date, activity_name, start_time, end_time, status_name, activity_note, employee.supervisor_lname, event_date, employee.first_name, employee.last_name FROM ex_event, employee WHERE ex_event.employee_id = employee.employee_id AND employee.department_name = @department AND status_name = 'Pending' AND event_date < DATEADD(day,-3,GETDATE())";
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@department", dept);

            

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("No events found!");
                }

                while (reader.Read())
                {
                    ManagerExEvent myMgrExEvent = new ManagerExEvent();
                                        
                    myMgrExEvent.eventID = reader.GetInt32(0);
                    myMgrExEvent.employeeID = reader.GetInt32(1);
                    myMgrExEvent.department = reader.GetString(2);
                    myMgrExEvent.eventDate = reader.GetDateTime(4);
                    myMgrExEvent.submissionDate = reader.GetDateTime(5);
                    myMgrExEvent.activityName = reader.GetString(6);
                    myMgrExEvent.startTime = reader.GetString(7);
                    myMgrExEvent.endTime = reader.GetString(8);
                    string duration = (Convert.ToDateTime(myMgrExEvent.endTime) - Convert.ToDateTime(myMgrExEvent.startTime)).ToString("hh\\:mm");
                    myMgrExEvent.duration = duration;
                    myMgrExEvent.statusName = reader.GetString(9);
                    myMgrExEvent.activityNote = reader.GetString(10);
                    myMgrExEvent.supLastName = reader.GetString(11);
                    myMgrExEvent.agentName = reader.GetString(14) + ", " + reader.GetString(13);
                    myList.Add(myMgrExEvent);
                }
                

            }
            catch 
            {
                
            }
            finally
            {
                conn.Close();
            }

            return myList;
        }


        public static List<ExEvent> SelectCompletedEvents()
        {
            var myList = new List<ExEvent>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = "SELECT event_id, ex_event.employee_id, employee.employee_id, event_date, submission_date, activity_name, start_time, end_time, status_name, activity_note, completed_date, completed_by, first_name, last_name FROM ex_event, employee WHERE status_name = 'Completed' AND ex_event.employee_id = employee.employee_id";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new ApplicationException("No events found!");
                }
                while (reader.Read())
                {
                    var item = new ExEvent();

                    item.eventID = reader.GetInt32(0);
                    item.employeeID = reader.GetInt32(2);
                    item.eventDate = reader.GetDateTime(3);
                    item.submissionDate = reader.GetDateTime(4);
                    item.activityName = reader.GetString(5);
                    item.startTime = reader.GetString(6);
                    item.endTime = reader.GetString(7);
                    item.duration = (Convert.ToDateTime(item.endTime) - Convert.ToDateTime(item.startTime)).ToString("hh\\:mm");
                    item.statusName = reader.GetString(8);
                    item.activityNote = reader.GetString(9);
                    if (!reader.IsDBNull(10))
                    {
                        item.completedDate = reader.GetDateTime(10);
                    }
                    if (!reader.IsDBNull(11))
                    {
                        item.completedBy = reader.GetString(11);
                    }
                    item.agentName = reader.GetString(13) + ", " + reader.GetString(12);
                    myList.Add(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return myList;
        }
        public static ExEvent SelectSingleEvent(int eventID)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = "SELECT employee_id, event_date, submission_date, activity_name, start_time, end_time, status_name, activity_note, completed_date, completed_by FROM ex_event WHERE event_id = @eventID";
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@eventID", eventID);

            ExEvent myExEvent = new ExEvent();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    myExEvent.eventID = eventID;
                    myExEvent.employeeID = reader.GetInt32(0);
                    myExEvent.eventDate = reader.GetDateTime(1);
                    myExEvent.submissionDate = reader.GetDateTime(2);
                    myExEvent.activityName = reader.GetString(3);
                    myExEvent.startTime = reader.GetString(4);
                    myExEvent.endTime = reader.GetString(5);
                    myExEvent.duration = (Convert.ToDateTime(myExEvent.endTime) - Convert.ToDateTime(myExEvent.startTime)).ToString("hh\\:mm");
                    myExEvent.statusName = reader.GetString(6);
                    myExEvent.activityNote = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                    {
                        myExEvent.completedDate = reader.GetDateTime(8);
                    }
                    if (!reader.IsDBNull(9))
                    {
                        myExEvent.completedBy = reader.GetString(9);
                    }

                }
                else
                {
                    var ax = new ApplicationException("Event not found!");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return myExEvent;
        }
        public static List<ExEvent> SelectEventsByAgent(int employeeID, string statusName, DateTime startDate, DateTime endDate)
        {
            var myList = new List<ExEvent>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = "SELECT event_id, ex_event.employee_id, employee.employee_id, event_date, submission_date, activity_name, start_time, end_time, status_name, activity_note, completed_date, completed_by, first_name, last_name FROM ex_event, employee WHERE ex_event.employee_id = @employeeID AND status_name = @statusName AND event_date >= @startDate AND event_date < @endDate AND ex_event.employee_id = employee.employee_id";
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@employeeID", employeeID);
            cmd.Parameters.AddWithValue("@statusName", statusName);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new ApplicationException("No events found!");
                }
                while (reader.Read())
                {
                    var item = new ExEvent();

                    item.eventID = reader.GetInt32(0);
                    item.employeeID = reader.GetInt32(2);
                    item.eventDate = reader.GetDateTime(3);
                    item.submissionDate = reader.GetDateTime(4);
                    item.activityName = reader.GetString(5);
                    item.startTime = reader.GetString(6);
                    item.endTime = reader.GetString(7);
                    item.duration = (Convert.ToDateTime(item.endTime) - Convert.ToDateTime(item.startTime)).ToString("hh\\:mm");
                    item.statusName = reader.GetString(8);
                    item.activityNote = reader.GetString(9);
                    if (!reader.IsDBNull(10))
                    {
                        item.completedDate = reader.GetDateTime(10);
                    }
                    if (!reader.IsDBNull(11))
                    {
                        item.completedBy = reader.GetString(11);
                    }
                    item.agentName = reader.GetString(13) + ", " + reader.GetString(12);
                    myList.Add(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return myList;
        }

        public static int InsertExEvent(ExEvent e)
        {
            var rowsAffected = 0;

            var conn = DatabaseConnection.GetExEventDatabaseConnection();

            string commandText = "INSERT INTO ex_event (event_date, employee_id, submission_date, activity_name, start_time, end_time, status_name, activity_note) VALUES (@event_date, @employee_id, @submission_date, @activity_name, @start_time, @end_time, @status_name, @activity_note)";

            var cmd = new SqlCommand(commandText, conn);

            cmd.Parameters.AddWithValue("@event_date", e.eventDate);
            cmd.Parameters.AddWithValue("@employee_id", e.employeeID);
            cmd.Parameters.AddWithValue("@submission_date", e.submissionDate);
            cmd.Parameters.AddWithValue("@activity_name", e.activityName);
            cmd.Parameters.AddWithValue("@start_time", e.startTime);
            cmd.Parameters.AddWithValue("@end_time", e.endTime);
            cmd.Parameters.AddWithValue("@status_name", e.statusName);
            cmd.Parameters.AddWithValue("@activity_note", e.activityNote);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    var up = new ApplicationException("Exception could not be submitted.");
                    throw up;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public static int UpdateSubmission(ExEvent oldE, ExEvent newE)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spUpdateSubmission";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@event_id", newE.eventID);
            cmd.Parameters.AddWithValue("@event_date", newE.eventDate);
            cmd.Parameters.AddWithValue("@submission_date", newE.submissionDate);
            cmd.Parameters.AddWithValue("@activity_name", newE.activityName);
            cmd.Parameters.AddWithValue("@start_time", newE.startTime);
            cmd.Parameters.AddWithValue("@end_time", newE.endTime);
            cmd.Parameters.AddWithValue("@status_name", newE.statusName);
            cmd.Parameters.AddWithValue("@activity_note", newE.activityNote);

            cmd.Parameters.AddWithValue("@completed_date", newE.completedDate);
            cmd.Parameters.AddWithValue("@completed_by", newE.completedBy);

            cmd.Parameters.AddWithValue("@original_event_id", oldE.eventID);
            cmd.Parameters.AddWithValue("@original_event_date", oldE.eventDate);
            cmd.Parameters.AddWithValue("@original_submission_date", oldE.submissionDate);
            cmd.Parameters.AddWithValue("@original_activity_name", oldE.activityName);
            cmd.Parameters.AddWithValue("@original_start_time", oldE.startTime);
            cmd.Parameters.AddWithValue("@original_end_time", oldE.endTime);
            cmd.Parameters.AddWithValue("@original_status_name", oldE.statusName);
            cmd.Parameters.AddWithValue("@original_activity_note", oldE.activityNote);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Exception:\\nYour record has been changed by another user.\nPlease refresh and try again.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public static int UpdateSubmissionToPending(ExEvent oldE, ExEvent newE)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spUpdateSubmissionToPending";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@event_id", newE.eventID);
            cmd.Parameters.AddWithValue("@event_date", newE.eventDate);
            cmd.Parameters.AddWithValue("@submission_date", newE.submissionDate);
            cmd.Parameters.AddWithValue("@activity_name", newE.activityName);
            cmd.Parameters.AddWithValue("@start_time", newE.startTime);
            cmd.Parameters.AddWithValue("@end_time", newE.endTime);
            cmd.Parameters.AddWithValue("@status_name", newE.statusName);
            cmd.Parameters.AddWithValue("@activity_note", newE.activityNote);

            cmd.Parameters.AddWithValue("@original_event_id", oldE.eventID);
            cmd.Parameters.AddWithValue("@original_event_date", oldE.eventDate);
            cmd.Parameters.AddWithValue("@original_submission_date", oldE.submissionDate);
            cmd.Parameters.AddWithValue("@original_activity_name", oldE.activityName);
            cmd.Parameters.AddWithValue("@original_start_time", oldE.startTime);
            cmd.Parameters.AddWithValue("@original_end_time", oldE.endTime);
            cmd.Parameters.AddWithValue("@original_status_name", oldE.statusName);
            cmd.Parameters.AddWithValue("@original_activity_note", oldE.activityNote);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Exception:\\nYour record has been changed by another user.\nPlease refresh and try again.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public static int DeleteExEvent(ExEvent eventToDelete)
        {
            int rowsAffected = 0;

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spDeleteEvent";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@original_event_id", eventToDelete.eventID);

            try
            {
                conn.Open();
                rowsAffected = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public static List<Activity> SelectActivityList()
        {
            var activityList = new List<Activity>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT activity_name FROM activity";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var newactivity = new Activity();

                        newactivity.activityName = reader.GetString(0);

                        activityList.Add(newactivity);
                    }
                }
                else
                {
                    var ax = new ApplicationException("No employee records were found");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return activityList;
        }

        public static List<Status> SelectStatusList()
        {
            var StatusList = new List<Status>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT Status_name FROM Status";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var newStatus = new Status();

                        newStatus.statusName = reader.GetString(0);

                        StatusList.Add(newStatus);
                    }
                }
                else
                {
                    var ax = new ApplicationException("No employee records were found");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return StatusList;
        }
    }
}
