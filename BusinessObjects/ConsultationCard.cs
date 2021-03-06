﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ConsultationCard
    {
        public int EmployeeID { get; set; }
        public int Communication { get; set; }
        public int Competitors { get; set; }
        public int Goals { get; set; }
        public int Growth { get; set; }
        public int Headcount { get; set; }
        public int Market { get; set; }
        public int Rapport { get; set; }
        public int Recommended { get; set; }
        public int Term { get; set; }
        public int Website { get; set; }
        public int TotalEntries { get; set; }
        public int LifetimeEntries { get; set; }
        public string CommunicationRequestDate { get; set; }
        public string CompetitorsRequestDate { get; set; }
        public string GoalsRequestDate { get; set; }
        public string GrowthRequestDate { get; set; }
        public string HeadcountRequestDate { get; set; }
        public string MarketRequestDate { get; set; }
        public string RapportRequestDate { get; set; }
        public string RecommendedRequestDate { get; set; }
        public string TermRequestDate { get; set; }
        public string WebsiteRequestDate { get; set; }
        public int NumEarned { get; set; }
    }
}
