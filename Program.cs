using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IO.Enumeration;
using pruebaJson;
using pruebaJson.Etities;

internal class Program
{
    private static void Main(string[] args)
    {
        string filePath = "Student.json";
        List<Student> listStudents = new List<Student>();
        try
        {
            listStudents = MyFunctions.LoadCsharpToJsonFile<List<Student>>(filePath);
        }
        catch
        {
            MyFunctions.ExportCsharpListToJson(listStudents, filePath);
        }
        bool loopMenu = true;
        while (loopMenu)
        {
            sbyte mainMenu = MyFunctions.MainMenu();
            if (mainMenu < 0 || mainMenu > 3)
            {
                MyFunctions.Invalid();
            }
            else
            {
                switch (mainMenu)
                {
                    case 1:
                        bool loopStudents = true;
                        while (loopStudents)
                        {
                            sbyte studentsMenu = MyFunctions.StudentsMenu();
                            if (studentsMenu < 0 || studentsMenu > 3)
                            {
                                MyFunctions.Invalid();
                            }
                            else
                            {
                                switch (studentsMenu)
                                {
                                    case 1:
                                        MyFunctions.AddStudent(listStudents);
                                        break;
                                    case 2:
                                        MyFunctions.RemoveStudent(listStudents);
                                        break;
                                    case 3:
                                        MyFunctions.UpdateStudent(listStudents);
                                        break;
                                    case 0:
                                        Console.Clear();
                                        loopStudents = false;
                                        break;
                                    default:
                                        MyFunctions.Invalid();
                                        break;
                                }
                            }
                        }
                        break;
                    case 2:
                        bool loopGrades = true;
                        while (loopGrades)
                        {
                            sbyte GradesMenu = MyFunctions.GradesMenu();
                            if (GradesMenu < 0 || GradesMenu > 3)
                            {
                                MyFunctions.Invalid();
                            }
                            else
                            {
                                switch (GradesMenu)
                                {
                                    case 1:
                                        MyFunctions.AddGrade(listStudents);
                                        break;
                                    case 2:
                                        MyFunctions.RemoveGrade(listStudents);
                                        break;
                                    case 3:
                                        MyFunctions.UpdateGrade(listStudents);
                                        break;
                                    case 0:
                                        Console.Clear();
                                        loopGrades = false;
                                        break;
                                    default:
                                        MyFunctions.Invalid();
                                        break;
                                }
                            }
                        }
                        break;
                    case 3:
                        bool loopGraphics = true;
                        while (loopGraphics)
                        {
                            sbyte GraphicsMenu = MyFunctions.GraphicsMenu();
                            if (GraphicsMenu < 0 || GraphicsMenu > 3)
                            {
                                MyFunctions.Invalid();
                            }
                            else
                            {
                                switch (GraphicsMenu)
                                {
                                    case 1:
                                        MyFunctions.StudentsInformationGraphic(listStudents);
                                        break;
                                    case 2:
                                        MyFunctions.GeneralGradesGraphic(listStudents);
                                        break;
                                    case 3:
                                        MyFunctions.FinalGradesGraphic(listStudents);
                                        break;
                                    case 0:
                                        Console.Clear();
                                        loopGraphics = false;
                                        break;
                                    default:
                                        MyFunctions.Invalid();
                                        break;
                                }
                            }
                        }
                        break;
                    case 0:
                        Console.Clear();
                        loopMenu = false;
                        break;
                    default:
                        MyFunctions.Invalid();
                        break;
                }
            }
        }
    }
}