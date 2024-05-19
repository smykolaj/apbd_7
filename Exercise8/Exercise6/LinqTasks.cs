using System.Collections;
using Exercise6.Models;

namespace Exercise6
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            #region Load depts

            List<Dept> depts =
            [
                new Dept
                {
                    Deptno = 1,
                    Dname = "Research",
                    Loc = "Warsaw"
                },
                new Dept
                {
                    Deptno = 2,
                    Dname = "Human Resources",
                    Loc = "New York"
                },
                new Dept
                {
                    Deptno = 3,
                    Dname = "IT",
                    Loc = "Los Angeles"
                }
            ];

            Depts = depts;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            List<Emp> emps =
            [
                e1, e2, e3, e4, e5, e6, e7, e8, e9, e10
            ];

            Emps = emps;

            #endregion
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public static IEnumerable<Emp> Task1()
        {

            var methodSyntax =
                Emps
                    .Where(s => s.Job.Equals("Backend programmer"));

            //Query syntax
            var querySyntax =
                from e in Emps
                where e.Job.Equals( "Backend programmer")
                select e;
            
            
            IEnumerable<Emp> result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public static IEnumerable<Emp> Task2()
        {
            
            // Method syntax
            var methodSyntax =
                Emps
                    .Where(s => s.Job.Equals("Frontend programmer") && s.Salary > 1000)
                    .OrderByDescending( e => e.Ename);
            
            //Query syntax
            var querySyntax =
                from e in Emps
                where e.Job.Equals("Frontend programmer") && e.Salary > 1000
                orderby e.Ename descending 
                select e;
            
            
            IEnumerable<Emp> result = querySyntax;
            return result;
        }


        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public static int Task3()
        {
            // Method syntax
            var methodSyntax =
                Emps.Max(e => e.Salary);

            //Query syntax
            //max IS NOT supported with query syntax
            
            int result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public static IEnumerable<Emp> Task4()
        {
            // Method syntax
            var methodSyntax =
                Emps
                    .Where(e => e.Salary.Equals(Task3()));

            IEnumerable<Emp> result = methodSyntax;
            return result;
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public static IEnumerable<object> Task5()
        {

            var a = new { Nazwisko = "Smith", Praca = "Front" };
            // Method syntax
            var methodSyntax =
                Emps.Select(e => new { Nazwisko = e.Ename, Praca = e.Job });
            
            //Query syntax
            var querySyntax =
                from e in Emps
                select new { Nazwisko = e.Ename, Praca = e.Job };
            
            IEnumerable<object> result = querySyntax;
            return result;
        }

        /// <summary>
        ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        ///     The result: merge of Emps and Depts collections.
        /// </summary>
        public static IEnumerable<object> Task6()
        {
            var methodSyntax =
                Emps.Join(Depts,
                    emp => emp.Deptno,
                    dept => dept.Deptno,
                    (emp, dept) => new
                        { 
                            emp.Ename,
                            emp.Job,
                            dept.Dname
                            
                        });
            
            //Query syntax
            var querySyntax =
                from emp in Emps // outer sequence
                join dept in Depts //inner sequence 
                    on emp.Deptno equals dept.Deptno // key selector 
                select new { // result selector 
                    emp.Ename,
                    emp.Job,
                    dept.Dname
                };
            
            
            IEnumerable<object> result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public static IEnumerable<object> Task7()
        {
            // Method syntax
            var methodSyntax =
                Emps.GroupBy(e => e.Job).Select(group => new
                {
                    Praca = group.Key,
                    LiczbaPracownikow = group.Count()
                });


            //Query syntax
            var querySyntax =
                from e in Emps
                group e by e.Job into grp
                select new {Praca = grp.Key, LiczbaPracownikow = grp.Count() };
            
            
            IEnumerable<object> result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     Return the value "true" if at least one
        ///     of the elements in the collection works as a "Backend programmer".
        /// </summary>
        public static bool Task8()
        {
            // Method syntax
            var methodSyntax =
                Emps.Any(e => e.Job.Equals("Backend programmer"));
            
            //Query syntax
            // not supported

            bool result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        public static Emp Task9()
        {
            // Method syntax
            var methodSyntax =
                Emps
                    .Where(s => s.Job.Equals("Frontend programmer")).MaxBy(e => e.HireDate);
            
            //Query syntax
            //first or default is not supported
            
            
            Emp result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak wartości", null, null;
        /// </summary>
        public static IEnumerable<object> Task10()
        {
            var a = new { Ename = "Brak wartości", Job = string.Empty, HireDate = (DateTime?)null };
            var methodSyntax = Emps.Select(e => new { e.Ename, e.Job, e.HireDate }).Union(from emp in Emps
                select a);
            var methodSyntax2 = Emps.Select(e => new { e.Ename, e.Job, e.HireDate }).Append(a);
                        
            
            IEnumerable<object> result = methodSyntax2;
            return result;
        }

        /// <summary>
        /// Using LINQ, retrieve employees divided by departments, keeping in mind that:
        /// 1. We are only interested in departments with more than 1 employee
        /// 2. We want to return a list of objects with the following structure:
        ///    [
        ///      {name: "RESEARCH", numOfEmployees: 3},
        ///      {name: "SALES", numOfEmployees: 5},
        ///      ...
        ///    ]
        /// 3. Use anonymous types
        /// </summary>
        public static IEnumerable<object> Task11()
        {
            var methodSyntax = Emps
                    .GroupBy(emp => emp.Deptno)
                    .Where(g => g.Count() > 1)
                    .Select(g => new
                    {
                        id = g.Key,
                        numOfEmployees = g.Count()
                    }).Join(Depts,
                        emp => emp.id,
                        dept => dept.Deptno,
                        (emp, dept) => new
                        { 
                            name = dept.Dname,
                            emp.numOfEmployees
                            
                        })
                ;
            
            IEnumerable<object> result = methodSyntax;
            return result;
        }

        /// <summary>
        /// Write your own extension method that will allow the following code snippet to compile.
        /// Add the method to the CustomExtensionMethods class, which is defined below.
        ///
        /// The method should return only those employees who have at least 1 direct subordinate.
        /// Employees should be sorted within the collection by surname (ascending) and salary (descending).
        /// </summary>
        public static IEnumerable<Emp> Task12()
        {
            var methodSyntax = Emps
                .Where(e1 => Emps.Contains
                    (e1, new CustomExtensionMethods.EmpsMgrComparer())
                ).OrderBy(e => e.Ename).ThenByDescending(e => e.Salary);
            
            
            IEnumerable<Emp> result = methodSyntax;
            return result;
        }

        /// <summary>
        /// The method below should return a single int number.
        /// It takes a list of integers as input.
        /// Try to find, using LINQ, the number that appears an odd number of times in the array of ints.
        /// It is assumed that there will always be one such number.
        /// For example: {1,1,1,1,1,1,10,1,1,1,1} => 10
        /// </summary>
        public static int Task13(int[] arr)
        {
            int result = arr
                .GroupBy(e => e)
                .Where(e => e.Count()%2 != 0)
                .Select(e => e.Key)
                .First();
            return result;
        }

        /// <summary>
        /// Return only those departments that have exactly 5 employees or no employees at all.
        /// Sort the result by department name in ascending order.
        /// </summary>
        public static IEnumerable<Dept> Task14()
        {
            var result = Depts.GroupJoin(Emps,
                                                dept => dept.Deptno,
                                                emp => emp.Deptno,
                                                (dept, emps) => new
                                                {
                                                    OneDept = dept, 
                                                    EmployeesInOneDept = emps
                                                }
                                            )
                .Where(g => g.EmployeesInOneDept.Count() == 5 || !g.EmployeesInOneDept.Any())
                .OrderBy(g => g.OneDept.Dname)
                .Select(g => g.OneDept);
            return result;
        }
    }

    public static class CustomExtensionMethods
    {
        public class EmpsMgrComparer : IEqualityComparer<Emp>
        {
            public bool Equals(Emp x, Emp y)
            {
                try
                {
                    if (y.Empno == x.Mgr.Empno)
                        return true;
                }
                catch (Exception e)
                {
                    return false;
                }
               

                return false;
            }

            public int GetHashCode(Emp obj)
            {
                return obj.GetHashCode();
            }
        }
        //Put your extension methods here
    }
}