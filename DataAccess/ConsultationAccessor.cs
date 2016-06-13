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
            var query = @"SELECT employee_id, communication, competitors, goals, growth, headcount, market, rapport, recommended, term, website, total_entries, lifetime_entries FROM consultation WHERE employee_id = @employeeID";
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
    }
}
