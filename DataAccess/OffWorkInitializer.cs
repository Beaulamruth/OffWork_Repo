using OffWork.Models;
using System;
using System.Collections.Generic;

namespace OffWork.DataAccess
{
    public class OffWorkInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<OffWorkContext>
    {
        protected override void Seed(OffWorkContext context)
        {
            var employees = new List<Employee>
            {
                new Employee{FirstName="Anand",LastName="Mahin",HireDate=DateTime.Parse("2015-09-01")},
                new Employee{FirstName="Mahendra",LastName="Dhoni",HireDate=DateTime.Parse("2012-09-01")},
                new Employee{FirstName="Jati",LastName="Ratnalu",HireDate=DateTime.Parse("2013-09-01")},
                new Employee{FirstName="Zinedine",LastName="Zidane",HireDate=DateTime.Parse("2012-09-01")},
                new Employee{FirstName="Elli",LastName="Hayden",HireDate=DateTime.Parse("2012-09-01")},
                new Employee{FirstName="Somany",LastName="Khan",HireDate=DateTime.Parse("2010-09-01")},
                new Employee{FirstName="Lakshmi",LastName="Norman",HireDate=DateTime.Parse("2009-09-01")}
            };

            employees.ForEach(e => context.Employee.Add(e));
            context.SaveChanges();

            var absenceTypes = new List<AbsenceType>
            {
                new AbsenceType{AbsenceTypeId=1,AbsenceTypeValue="Sick day",AbsenceTypeCode="SD"},
                new AbsenceType{AbsenceTypeId=2,AbsenceTypeValue="Personal day",AbsenceTypeCode="PD"},
                new AbsenceType{AbsenceTypeId=3,AbsenceTypeValue="Vacation day",AbsenceTypeCode="VD"},
                new AbsenceType{AbsenceTypeId=4,AbsenceTypeValue="Unpaid day",AbsenceTypeCode="UD"},
                new AbsenceType{AbsenceTypeId=5,AbsenceTypeValue="Remote day",AbsenceTypeCode="RD"}
            };
            absenceTypes.ForEach(a => context.AbsenceType.Add(a));
            context.SaveChanges();

            //var absenceDetails = new List<AbsenceDetail>
            //{
            //    new AbsenceDetail{
            //        AbsenceDetailId = 1,
            //        AbsenceTypeId = 5,
            //        EmployeeId = 1,
            //        StartDateOfAbsence = DateTime.Today,
            //        EndDateOfAbsence = DateTime.Today
            //    },
            //};
            //absenceDetails.ForEach(d => context.AbsenceDetail.Add(d));
            //context.SaveChanges();
        }
    }
}