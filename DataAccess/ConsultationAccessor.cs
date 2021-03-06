﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Specialized;

namespace DataAccess
{
    public class ConsultationAccessor
    {
        public static ConsultationCard SelectCard(int employeeID)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, communication, competitors, goals, growth, headcount, market, rapport, recommended, term, website, total_entries, lifetime_entries, communication_request_date, competitors_request_date, goals_request_date, growth_request_date, headcount_request_date, market_request_date, rapport_request_date, recommended_request_date, term_request_date, website_request_date FROM consultation WHERE employee_id = @employeeID";
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

                retrievedCard.CommunicationRequestDate = reader.GetSqlDateTime(13).ToString();
                retrievedCard.CompetitorsRequestDate = reader.GetSqlDateTime(14).ToString();
                retrievedCard.GoalsRequestDate = reader.GetSqlDateTime(15).ToString();
                retrievedCard.GrowthRequestDate = reader.GetSqlDateTime(16).ToString();
                retrievedCard.HeadcountRequestDate = reader.GetSqlDateTime(17).ToString();
                retrievedCard.MarketRequestDate = reader.GetSqlDateTime(18).ToString();
                retrievedCard.RapportRequestDate = reader.GetSqlDateTime(19).ToString();
                retrievedCard.RecommendedRequestDate = reader.GetSqlDateTime(20).ToString();
                retrievedCard.TermRequestDate = reader.GetSqlDateTime(21).ToString();
                retrievedCard.WebsiteRequestDate = reader.GetSqlDateTime(22).ToString();

                int numCards = 0;

                if (retrievedCard.Communication == 1)
                {
                    numCards += 1;
                }
                if (retrievedCard.Competitors == 1)
                {
                    numCards += 1;
                }
                if (retrievedCard.Goals == 1)
                {
                    numCards += 1;
                }
                if (retrievedCard.Growth == 1)
                {
                    numCards += 1;
                }
                if (retrievedCard.Headcount == 1)
                {
                    numCards += 1;
                }
                if (retrievedCard.Market == 1)
                {
                    numCards += 1;
                }
                if (retrievedCard.Rapport == 1)
                {
                    numCards += 1;
                }
                if (retrievedCard.Recommended == 1)
                {
                    numCards += 1;
                }
                if (retrievedCard.Term == 1)
                {
                    numCards += 1;
                }
                if (retrievedCard.Website == 1)
                {
                    numCards += 1;
                }

                int entryTotal = retrievedCard.TotalEntries * 10;
                int totalPoints = entryTotal + numCards;

                retrievedCard.NumEarned = totalPoints;

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
            cmd.Parameters.AddWithValue("@communication_request_date", newC.CommunicationRequestDate);
            cmd.Parameters.AddWithValue("@competitors_request_date", newC.CompetitorsRequestDate);
            cmd.Parameters.AddWithValue("@goals_request_date", newC.GoalsRequestDate);
            cmd.Parameters.AddWithValue("@growth_request_date", newC.GrowthRequestDate);
            cmd.Parameters.AddWithValue("@headcount_request_date", newC.HeadcountRequestDate);
            cmd.Parameters.AddWithValue("@market_request_date", newC.MarketRequestDate);
            cmd.Parameters.AddWithValue("@rapport_request_date", newC.RapportRequestDate);
            cmd.Parameters.AddWithValue("@recommended_request_date", newC.RecommendedRequestDate);
            cmd.Parameters.AddWithValue("@term_request_date", newC.TermRequestDate);
            cmd.Parameters.AddWithValue("@website_request_date", newC.WebsiteRequestDate);

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
            cmd.Parameters.AddWithValue("@original_communication_request_date", oldC.CommunicationRequestDate);
            cmd.Parameters.AddWithValue("@original_competitors_request_date", oldC.CompetitorsRequestDate);
            cmd.Parameters.AddWithValue("@original_goals_request_date", oldC.GoalsRequestDate);
            cmd.Parameters.AddWithValue("@original_growth_request_date", oldC.GrowthRequestDate);
            cmd.Parameters.AddWithValue("@original_headcount_request_date", oldC.HeadcountRequestDate);
            cmd.Parameters.AddWithValue("@original_market_request_date", oldC.MarketRequestDate);
            cmd.Parameters.AddWithValue("@original_rapport_request_date", oldC.RapportRequestDate);
            cmd.Parameters.AddWithValue("@original_recommended_request_date", oldC.RecommendedRequestDate);
            cmd.Parameters.AddWithValue("@original_term_request_date", oldC.TermRequestDate);
            cmd.Parameters.AddWithValue("@original_website_request_date", oldC.WebsiteRequestDate);

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

        public static int UpdateConsultationEntries(ConsultationCard oldC, ConsultationCard newC)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spUpdateEntries";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employee_id", newC.EmployeeID);
            cmd.Parameters.AddWithValue("@total_entries", newC.TotalEntries);
            cmd.Parameters.AddWithValue("@lifetime_entries", newC.LifetimeEntries);

            cmd.Parameters.AddWithValue("@original_employee_id", oldC.EmployeeID);
            cmd.Parameters.AddWithValue("@original_total_entries", oldC.TotalEntries);
            cmd.Parameters.AddWithValue("@original_lifetime_entries", oldC.LifetimeEntries);

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

        public static int CreateConsultationCard(ConsultationCard newC)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spCreateConsultationCard";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employee_id", newC.EmployeeID);
            cmd.Parameters.AddWithValue("@communication_request_date", newC.CommunicationRequestDate);
            cmd.Parameters.AddWithValue("@competitors_request_date", newC.CompetitorsRequestDate);
            cmd.Parameters.AddWithValue("@goals_request_date", newC.GoalsRequestDate);
            cmd.Parameters.AddWithValue("@growth_request_date", newC.GrowthRequestDate);
            cmd.Parameters.AddWithValue("@headcount_request_date", newC.HeadcountRequestDate);
            cmd.Parameters.AddWithValue("@market_request_date", newC.MarketRequestDate);
            cmd.Parameters.AddWithValue("@rapport_request_date", newC.RapportRequestDate);
            cmd.Parameters.AddWithValue("@recommended_request_date", newC.RecommendedRequestDate);
            cmd.Parameters.AddWithValue("@term_request_date", newC.TermRequestDate);
            cmd.Parameters.AddWithValue("@website_request_date", newC.WebsiteRequestDate);

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

        public static int CreateNewCardSheet(int EmployeeID)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spCreateNewCardSheet";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employee_id", EmployeeID);
            cmd.Parameters.AddWithValue("@created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Card Creation Failed. Please refresh and try again.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public static int InsertCard(SheetCard newCard)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spInsertCard";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sheet_id", newCard.sheetID);
            cmd.Parameters.AddWithValue("@card_slot", newCard.cardSlot);
            cmd.Parameters.AddWithValue("@card_name", newCard.cardName);
            cmd.Parameters.AddWithValue("@requested_date", newCard.requestedDate);
            cmd.Parameters.AddWithValue("@award_date", newCard.awardDate);
            cmd.Parameters.AddWithValue("@awarded_by", newCard.awardedBy);
            cmd.Parameters.AddWithValue("@award_method", newCard.awardMethod);
            cmd.Parameters.AddWithValue("@award_note", newCard.awardNote);
            
            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Card Insert Failed. Please refresh and try again.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public static int CloseCardSheet(ConsultationSheet oldSheet, ConsultationSheet newSheet)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spCloseCardSheet";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sheet_id", newSheet.sheetID);
            cmd.Parameters.AddWithValue("@employee_id", newSheet.employeeID);
            cmd.Parameters.AddWithValue("@completed_date", newSheet.completedDate);

            cmd.Parameters.AddWithValue("@original_sheet_id", oldSheet.sheetID);
            cmd.Parameters.AddWithValue("@original_employee_id", oldSheet.employeeID);
            cmd.Parameters.AddWithValue("@original_completed_date", oldSheet.completedDate);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Card Close Failed. Please refresh and try again.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public static int RemoveCard(int sheetID, string cardName)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spRemoveCard";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sheet_id", sheetID);
            cmd.Parameters.AddWithValue("@card_name", cardName);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Card Delete Failed. Please refresh and try again.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public static List<CardMethod> SelectCardMethods()
        {
            var myList = new List<CardMethod>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = "SELECT method_name FROM award_method";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new ApplicationException("No methods found!");
                }
                while (reader.Read())
                {
                    var item = new CardMethod();

                    item.methodName = reader.GetString(0);
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

        public static ConsultationSheet SelectCurrentConsultationSheet(int employeeID)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT sheet_id, employee_id, created_date FROM card_sheets WHERE employee_id = @employeeID AND completed_date IS NULL";
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

                var retrievedSheet = new ConsultationSheet();
                reader.Read();
                retrievedSheet.sheetID = reader.GetInt32(0);
                retrievedSheet.employeeID = reader.GetInt32(1);
                retrievedSheet.createdDate = reader.GetSqlDateTime(2).ToString();


                return retrievedSheet;
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

        public static int SelectCurrentSheetCardCount(int sheetID)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT COUNT(card_slot) FROM cards WHERE sheet_id = @sheetID";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@sheetID", sheetID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Consultation Card not found.");
                }

                var numCards = 0;
                reader.Read();
                numCards = reader.GetInt32(0);


                return numCards;
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

        public static string SelectMostAwardedCard(int month)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT TOP 1 card_name FROM cards, card_sheets WHERE cards.sheet_id = card_sheets.sheet_id AND card_sheets.created_date > @monthStart GROUP BY card_name ORDER BY COUNT(*) DESC";
            var cmd = new SqlCommand(query, conn);
            string currentYear = DateTime.Now.Year.ToString();
            string monthStart = month + "/1/" + currentYear;
            
            cmd.Parameters.AddWithValue("@monthStart", monthStart);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Consultation Card not found.");
                }

                string cardName = "";
                reader.Read();
                cardName = reader.GetString(0);


                return cardName;
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

        public static string SelectLeastAwardedCard(int month)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT TOP 1 card_name FROM cards, card_sheets WHERE cards.sheet_id = card_sheets.sheet_id AND card_sheets.created_date > @monthStart GROUP BY card_name ORDER BY COUNT(*) ASC";
            var cmd = new SqlCommand(query, conn);
            string currentYear = DateTime.Now.Year.ToString();
            string monthStart = month + "/1/" + currentYear;
            cmd.Parameters.AddWithValue("@monthStart", monthStart);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Consultation Card not found.");
                }

                string cardName = "";
                reader.Read();
                cardName = reader.GetString(0);


                return cardName;
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

        public static List<string> SelectMostTargetedCards(int month)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT TOP 3 card_name FROM cards, card_sheets WHERE cards.sheet_id = card_sheets.sheet_id AND card_sheets.created_date > @monthStart AND card_slot = '1' OR card_slot = '2' OR card_slot = '3' GROUP BY card_name ORDER BY COUNT(*) DESC";
            var cmd = new SqlCommand(query, conn);
            string currentYear = DateTime.Now.Year.ToString();
            string monthStart = month + "/1/" + currentYear;
            cmd.Parameters.AddWithValue("@monthStart", monthStart);

            List<string> cardList = new List<string>();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new ApplicationException("No methods found!");
                }
                while (reader.Read())
                {
                    string item = "";

                    item = reader.GetString(0);
                    cardList.Add(item);
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
            return cardList;
        }

        public static List<string> SelectLeastTargetedCards(int month)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT TOP 3 card_name FROM cards, card_sheets WHERE cards.sheet_id = card_sheets.sheet_id AND card_sheets.created_date > @monthStart AND card_slot = '7' OR card_slot = '8' OR card_slot = '9' GROUP BY card_name ORDER BY COUNT(*) ASC";
            var cmd = new SqlCommand(query, conn);
            string currentYear = DateTime.Now.Year.ToString();
            string monthStart = month + "/1/" + currentYear;
            cmd.Parameters.AddWithValue("@monthStart", monthStart);

            List<string> cardList = new List<string>();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new ApplicationException("No methods found!");
                }
                while (reader.Read())
                {
                    string item = "";

                    item = reader.GetString(0);
                    cardList.Add(item);
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
            return cardList;
        }

        public static List<ConsultationSheet> SelectConsultationSheetDates(int month)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT created_date, completed_date FROM card_sheets WHERE created_date > @monthStart AND completed_date != '1/1/2000 12:00:00 PM' AND completed_date IS NOT NULL";
            var cmd = new SqlCommand(query, conn);
            string currentYear = DateTime.Now.Year.ToString();
            string monthStart = month + "/1/" + currentYear;
            cmd.Parameters.AddWithValue("@monthStart", monthStart);

            List<ConsultationSheet> sheetList = new List<ConsultationSheet>();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new ApplicationException("No methods found!");
                }
                while (reader.Read())
                {
                    ConsultationSheet currentSheet = new ConsultationSheet();

                    currentSheet.createdDate = reader.GetSqlDateTime(0).ToString();
                    currentSheet.completedDate = reader.GetSqlDateTime(1).ToString();
                    sheetList.Add(currentSheet);
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
            return sheetList;
        }

        public static int SelectCountByMethod(int month, string method)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT COUNT(award_method) FROM cards, card_sheets WHERE cards.sheet_id = card_sheets.sheet_id AND card_sheets.created_date > @monthStart AND cards.award_method = @awardMethod";
            var cmd = new SqlCommand(query, conn);
            string currentYear = DateTime.Now.Year.ToString();
            string monthStart = month + "/1/" + currentYear;
            cmd.Parameters.AddWithValue("@monthStart", monthStart);
            cmd.Parameters.AddWithValue("@awardMethod", method);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Consultation Card not found.");
                }

                var numCards = 0;
                reader.Read();
                numCards = reader.GetInt32(0);


                return numCards;
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

        public static NameValueCollection SelectTotalEntriesByDept(int month)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT department_name, COUNT(sheet_id) from employee, card_sheets WHERE card_sheets.employee_id = employee.employee_id AND created_date > @monthStart AND completed_date != '1/1/2000' AND completed_date IS NOT NULL ORDER BY COUNT(*) DESC";
            var cmd = new SqlCommand(query, conn);
            string currentYear = DateTime.Now.Year.ToString();
            string monthStart = month + "/1/" + currentYear;
            cmd.Parameters.AddWithValue("@monthStart", monthStart);

            NameValueCollection sheetList = new NameValueCollection();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new ApplicationException("No methods found!");
                }
                while (reader.Read())
                {
                    string dept = reader.GetString(0);
                    string sheetCount = reader.GetInt32(1).ToString();

                    sheetList[dept] = sheetCount.ToString();
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
            return sheetList;
        }

        public static int SelectTotalEntriesByDepartment(int month, string department)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT COUNT(sheet_id) from employee, card_sheets WHERE card_sheets.employee_id = employee.employee_id AND employee.department_name = @department AND created_date > @monthStart AND completed_date != '1/1/2000' AND completed_date IS NOT NULL";
            var cmd = new SqlCommand(query, conn);
            string currentYear = DateTime.Now.Year.ToString();
            string monthStart = month + "/1/" + currentYear;
            cmd.Parameters.AddWithValue("@monthStart", monthStart);
            cmd.Parameters.AddWithValue("@department", department);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Department Sheets Not Found.");
                }

                
                reader.Read();
                int numEntries = reader.GetInt32(0);


                return numEntries;
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

        public static int SelectTotalEntriesBySup(int month, Employee sup)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT COUNT(sheet_id) from employee, card_sheets WHERE card_sheets.employee_id = employee.employee_id AND employee.supervisor_fname = @supFirstName AND employee.supervisor_lname = @supLastName AND created_date > @monthStart AND completed_date != '1/1/2000' AND completed_date IS NOT NULL";
            var cmd = new SqlCommand(query, conn);
            string currentYear = DateTime.Now.Year.ToString();
            string monthStart = month + "/1/" + currentYear;
            cmd.Parameters.AddWithValue("@monthStart", monthStart);
            cmd.Parameters.AddWithValue("@supFirstName", sup.FirstName);
            cmd.Parameters.AddWithValue("@supLastName", sup.LastName);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Department Sheets Not Found.");
                }


                reader.Read();
                int numEntries = reader.GetInt32(0);


                return numEntries;
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

