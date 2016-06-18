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
    public class ConsultationAccessor
    {
        public static ConsultationCard SelectCard(int employeeID)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, communication, competitors, goals, growth, headcount, market, rapport, recommended, term, website, total_entries, lifetime_entries, request_date FROM consultation WHERE employee_id = @employeeID";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@employeeID", employeeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Consultation Card not found.");
                }

                var retrievedCard = new ConsultationCard();
                reader.Read();
                retrievedCard.EmployeeID = reader.GetInt32(0);
                retrievedCard.Communication = reader.GetInt32(1);
                retrievedCard.Competitors = reader.GetInt32(2);
                retrievedCard.Goals = reader.GetInt32(3);
                retrievedCard.Growth = reader.GetInt32(4);
                retrievedCard.Headcount = reader.GetInt32(5);
                retrievedCard.Market = reader.GetInt32(6);
                retrievedCard.Rapport = reader.GetInt32(7);
                retrievedCard.Recommended = reader.GetInt32(8);
                retrievedCard.Term = reader.GetInt32(9);
                retrievedCard.Website = reader.GetInt32(10);
                retrievedCard.TotalEntries = reader.GetInt32(11);
                retrievedCard.LifetimeEntries = reader.GetInt32(12);
                retrievedCard.RequestDate = reader.GetDateTime(13);

                return retrievedCard;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static int UpdateConsultationCard(ConsultationCard oldC, ConsultationCard newC)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spUpdateConsultationCard";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employee_id", newC.EmployeeID);
            cmd.Parameters.AddWithValue("@communication", newC.Communication);
            cmd.Parameters.AddWithValue("@competitors", newC.Competitors);
            cmd.Parameters.AddWithValue("@goals", newC.Goals);
            cmd.Parameters.AddWithValue("@growth", newC.Growth);
            cmd.Parameters.AddWithValue("@headcount", newC.Headcount);
            cmd.Parameters.AddWithValue("@market", newC.Market);
            cmd.Parameters.AddWithValue("@rapport", newC.Rapport);
            cmd.Parameters.AddWithValue("@recommended", newC.Recommended);
            cmd.Parameters.AddWithValue("@term", newC.Term);
            cmd.Parameters.AddWithValue("@website", newC.Website);
            cmd.Parameters.AddWithValue("@total_entries", newC.TotalEntries);
            cmd.Parameters.AddWithValue("@lifetime_entries", newC.LifetimeEntries);
            cmd.Parameters.AddWithValue("@request_date", newC.RequestDate);

            cmd.Parameters.AddWithValue("@original_employee_id", oldC.EmployeeID);
            cmd.Parameters.AddWithValue("@original_communication", oldC.Communication);
            cmd.Parameters.AddWithValue("@original_competitors", oldC.Competitors);
            cmd.Parameters.AddWithValue("@original_goals", oldC.Goals);
            cmd.Parameters.AddWithValue("@original_growth", oldC.Growth);
            cmd.Parameters.AddWithValue("@original_headcount", oldC.Headcount);
            cmd.Parameters.AddWithValue("@original_market", oldC.Market);
            cmd.Parameters.AddWithValue("@original_rapport", oldC.Rapport);
            cmd.Parameters.AddWithValue("@original_recommended", oldC.Recommended);
            cmd.Parameters.AddWithValue("@original_term", oldC.Term);
            cmd.Parameters.AddWithValue("@original_website", oldC.Website);
            cmd.Parameters.AddWithValue("@original_total_entries", oldC.TotalEntries);
            cmd.Parameters.AddWithValue("@original_lifetime_entries", oldC.LifetimeEntries);
            cmd.Parameters.AddWithValue("@original_request_date", oldC.LifetimeEntries);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Exception:Your record has been changed by another user. Please refresh and try again.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }
    }
}
