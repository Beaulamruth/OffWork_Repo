using System;
using System.ComponentModel.DataAnnotations;

namespace OffWork.Models
{
    public class AbsenceDetail
    {
       public AbsenceDetail()
        {
            StartDateFullDay = true;
            EndDateFullDay = true;

        }
        public int AbsenceDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int AbsenceTypeId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateOfAbsence { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateOfAbsence { get; set; }

        private bool stFullDay;
        public bool StartDateFullDay
        {
            get { return stFullDay; }
            set { stFullDay = value; }
        }

        private bool edFullDay;
        public bool EndDateFullDay {
            get { return edFullDay; }
            set { edFullDay = value; }
        }
        public bool IsApproved { get; set; }
        public virtual AbsenceType AbsenceType { get; set; }
        public virtual Employee Employee { get; set; }
    }
}