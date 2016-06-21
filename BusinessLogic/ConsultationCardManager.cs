using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace BusinessLogic
{
    public class ConsultationCardManager
    {
        public ConsultationCard FindCard(int employeeID)
        {
            try
            {
                var cardToFind = ConsultationAccessor.SelectCard(employeeID);
                return cardToFind;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem accessing the database.", ex);
            }
        }

        public void UpdateConsultationCard(ConsultationCard oldC, ConsultationCard newC)
        {
            try
            {
                ConsultationAccessor.UpdateConsultationCard(oldC, newC);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateConsultationCard(ConsultationCard newC)
        {
            try
            {
                ConsultationAccessor.CreateConsultationCard(newC);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateConsultationEntries(ConsultationCard oldC, ConsultationCard newC)
        {
            try
            {
                ConsultationAccessor.UpdateConsultationEntries(oldC, newC);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
