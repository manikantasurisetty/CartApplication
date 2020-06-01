using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartProjectDAL;
using CartProjectCommonLayer;
using Newtonsoft.Json;
namespace CardProjectBL
{
    public class EmployeeBL
    {
        public DataTable GetStudentBelow5()
        {
            StudentDAL studentDAL = new StudentDAL();


            return studentDAL.FetchSelectedStudents().Tables[0];



        }


    }

}
