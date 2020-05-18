using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIKONClassLibrary;

namespace SIKONClient
{
    class ViewModelKursusSide
    {
        public Event ID { get; set; }
        public Event Room_ID { get; set; }
        public Event Owner_ID { get; set; }
        public Event Subject { get; set; }
        public Event Category { get; set; }
        public Event Speaker { get; set; }
        public Event Description { get; set; }

        public Question Question_ID { get; set; }
        public Question Account_ID { get; set; }
        public Question Event_ID { get; set; }
        public Question Question_Subject { get; set; }
        public Question Question_Description  { get; set; }
        public Question Anonymity { get; set; }



        public ViewModelKursusSide()
        {
            
        }

        public ViewModelKursusSide(Event id, Event roomId, Event ownerId, Event subject, Event category, Event speaker, Event description, Question questionId, Question accountId, Question eventId, Question questionSubject, Question questionDescription, Question anonymity)
        {
            ID = id;
            Room_ID = roomId;
            Owner_ID = ownerId;
            Subject = subject;
            Category = category;
            Speaker = speaker;
            Description = description;
            Question_ID = questionId;
            Account_ID = accountId;
            Event_ID = eventId;
            Question_Subject = questionSubject;
            Question_Description = questionDescription;
            Anonymity = anonymity;
        }

    }
}
