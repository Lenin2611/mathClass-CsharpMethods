using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IO.Enumeration;
using pruebaJson;
using pruebaJson.Etities;
using System.Linq;
using System.Net;
using System.Globalization;

namespace pruebaJson;

public class MyFunctions
{
    public static void ExportCsharpListToJson<T>(List<T> list, string filePath)
    {
        string jsonString = JsonConvert.SerializeObject(list, Formatting.Indented);
        File.WriteAllText(filePath, jsonString);
    }

    public static T LoadCsharpToJsonFile<T>(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string jsonString = reader.ReadToEnd();
            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }

    public static void Invalid()
    {
        Console.Clear();
        Console.Write("Invalid Input. \n\nPress Enter to go back. . . ");
        Console.ReadKey();
        Console.Clear();
    }

    public static sbyte MainMenu()
    {
        try
        {
            Console.Clear();
            Console.Write("MAIN MENU \n\n1. Edit Students \n2. Edit Grades \n3. Graphics \n\n0. Exit \n\nOption: ");
            return Convert.ToSByte(Console.ReadLine());
        }
        catch
        {
            return 10;
        }
    }

    public static sbyte StudentsMenu()
    {
        try
        {
            Console.Clear();
            Console.Write("STUDENTS MENU \n\n1. Add Students \n2. Remove Student \n3. Update Student \n\n0. Back to Main Menu \n\nOption: ");
            return Convert.ToSByte(Console.ReadLine());
        }
        catch
        {
            return 10;
        }
    }

    public static ulong InsertId(List<Student> listStudents)
    {
        ulong newId = 0;
        bool loop = true;
        while (loop)
        {
            loop = false;
            try
            {
                Console.Write("\nStudent's Id: ");
                newId = ulong.Parse(Console.ReadLine());
                if (newId < 1 || newId > 999999999999999)
                {
                    Invalid();
                    loop = true;
                }
                else
                {
                    for (int i = 0; i < listStudents.Count; i++)
                    {
                        if (newId == listStudents[i].Id)
                        {
                            Invalid();
                            loop = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
                Invalid();
                loop = true;
            }
        }
        return newId;
    }

    public static string InsertName(List<Student> listStudents)
    {
        string newName = string.Empty;
        bool loop = true;
        while (loop)
        {
            loop = false;
            Console.Write("\nStudent's Name: ");
            newName = Console.ReadLine().ToLower();
            if (newName.Length < 1 || newName.Length > 40)
            {
                Invalid();
                loop = true;
            }
            if (!newName.All(char.IsAsciiLetter))
            {
                Invalid();
                loop = true;
            }
            else
            {
                for (int i = 0; i < (listStudents.Count - 1); i++)
                {
                    if (newName == listStudents[i].Name)
                    {
                        Invalid();
                        loop = true;
                        break;
                    }
                }
            }
        }
        return newName;
    }

    public static string InsertEmail()
    {
        string newEmail = string.Empty;
        bool loop = true;
        while (loop)
        {
            loop = false;
            Console.Write("\nStudent's Email: ");
            newEmail = Console.ReadLine().ToLower();
            if (newEmail.Length < 1 || newEmail.Length > 40)
            {
                Invalid();
                loop = true;
            }
        }
        return newEmail;
    }

    public static sbyte InsertAge()
    {
        sbyte newAge = 0;
        bool loop = true;
        while (loop)
        {
            loop = false;
            try
            {
                Console.Write("\nStudent's Age: ");
                newAge = Convert.ToSByte(Console.ReadLine());
                if (newAge < 1 || newAge > 99)
                {
                    Invalid();
                    loop = true;
                }
            }
            catch
            {
                Invalid();
                loop = true;
            }
        }
        return newAge;
    }

    public static string InsertAddress()
    {
        string newAddress = string.Empty;
        bool loop = true;
        while (loop)
        {
            loop = false;
            Console.Write("\nStudent's Address: ");
            newAddress = Console.ReadLine().ToLower();
            if (newAddress.Length < 1 || newAddress.Length > 30)
            {
                Invalid();
                loop = true;
            }
        }
        return newAddress;
    }

    public static void AddStudent(List<Student> listStudents)
    {
        bool loop = true;
        while (loop)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("ADD STUDENTS");
                Console.Write("\n0. Go back to Students Menu \n\nNumber of students to add (Max 100): ");
                sbyte quantity = Convert.ToSByte(Console.ReadLine());
                if (quantity == 0)
                {
                    loop = false;
                }
                if (quantity < 0 || quantity > 100)
                {
                    Invalid();
                }
                else
                {
                    for (sbyte i = 0; i < quantity; i++)
                    {
                        Student student = new Student();
                        Console.Clear();
                        student.Id = InsertId(listStudents);
                        Console.Clear();
                        student.Name = InsertName(listStudents);
                        Console.Clear();
                        student.Email = InsertEmail();
                        Console.Clear();
                        student.Age = InsertAge();
                        Console.Clear();
                        student.Address = InsertAddress();
                        listStudents.Add(student);
                        ExportCsharpListToJson(listStudents, "Student.json");
                        Console.Clear();
                        Console.WriteLine("ADD STUDENTS");
                        Console.Write("\nStudent {0} with Id {1} added. \n\nPress Enter to continue. . . ", student.Name.ToUpper(), student.Id);
                        Console.ReadKey();
                    }
                    loop = false;
                }
            }
            catch
            {
                Invalid();
            }
        }
    }

    public static void RemoveStudent(List<Student> listStudents)
    {
        bool loop = true;
        while (loop)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("REMOVE STUDENT\n");
                for (int i = 0; i < listStudents.Count; i++)
                {
                    Console.WriteLine("{0, -15} | {1, -40}", listStudents[i].Id, listStudents[i].Name);
                }
                Console.Write("\n0. Go back to Students Menu \n\nId of the student to remove: ");
                ulong idToRemove = ulong.Parse(Console.ReadLine());
                Student studentToRemove = listStudents.FirstOrDefault(student => student.Id.Equals(idToRemove));
                if (idToRemove == 0)
                {
                    loop = false;
                }
                else if (studentToRemove != null)
                {
                    listStudents.Remove(studentToRemove);
                    ExportCsharpListToJson(listStudents, "Student.json");
                    Console.Clear();
                    Console.WriteLine("REMOVE STUDENT\n");
                    Console.Write("Student {0} was removed. \n\nPress Enter to continue. . . ", studentToRemove.Name.ToUpper());
                    Console.ReadKey();
                    loop = false;
                }
                else
                {
                    Invalid();
                }
            }
            catch
            {
                Invalid();
            }
        }
    }

    public static void UpdateStudent(List<Student> listStudents)
    {
        bool loop = true;
        while (loop)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("UPDATE STUDENT\n");
                for (int i = 0; i < listStudents.Count; i++)
                {
                    Console.WriteLine("{0, -15} | {1, -40}", listStudents[i].Id, listStudents[i].Name);
                }
                Console.Write("\n0. Go back to Students Menu \n\nId of the student to update: ");
                ulong idToUpdate = ulong.Parse(Console.ReadLine());
                Student studentToUpdate = listStudents.FirstOrDefault(student => student.Id.Equals(idToUpdate));
                if (idToUpdate == 0)
                {
                    loop = false;
                }
                else if (studentToUpdate != null)
                {
                    bool loopOption = true;
                    while (loopOption)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("UPDATE STUDENT");
                            Console.Write("\n1. Update Name \n2. Update Email \n3. Update Age \n4. Update Address \n\n0. Go back \n\nOption: ");
                            sbyte option = sbyte.Parse(Console.ReadLine());
                            if (option > 0)
                            {
                                switch (option)
                                {
                                    case 1:
                                        Console.Clear();
                                        Console.WriteLine("Current Name: {0}", studentToUpdate.Name);
                                        studentToUpdate.Name = InsertName(listStudents);
                                        ExportCsharpListToJson(listStudents, "Student.json");
                                        Console.Clear();
                                        Console.WriteLine("UPDATE STUDENT\n");
                                        Console.Write("Student's Name was updated to {0}. \n\nPress Enter to continue. . . ", studentToUpdate.Name.ToUpper());
                                        Console.ReadKey();
                                        loopOption = false;
                                        break;
                                    case 2:
                                        Console.Clear();
                                        Console.WriteLine("Current Email: {0}", studentToUpdate.Email);
                                        studentToUpdate.Email = InsertEmail();
                                        ExportCsharpListToJson(listStudents, "Student.json");
                                        Console.Clear();
                                        Console.WriteLine("UPDATE STUDENT\n");
                                        Console.Write("Student's Email was updated to {0}. \n\nPress Enter to continue. . . ", studentToUpdate.Email.ToUpper());
                                        Console.ReadKey();
                                        loopOption = false;
                                        break;
                                    case 3:
                                        Console.Clear();
                                        Console.WriteLine("Current Age: {0}", studentToUpdate.Age);
                                        studentToUpdate.Age = InsertAge();
                                        ExportCsharpListToJson(listStudents, "Student.json");
                                        Console.Clear();
                                        Console.WriteLine("UPDATE STUDENT\n");
                                        Console.Write("Student's Age was updated {0}. \n\nPress Enter to continue. . . ", studentToUpdate.Age);
                                        Console.ReadKey();
                                        loopOption = false;
                                        break;
                                    case 4:
                                        Console.Clear();
                                        Console.WriteLine("Current Address: {0}", studentToUpdate.Address);
                                        studentToUpdate.Address = InsertAddress();
                                        ExportCsharpListToJson(listStudents, "Student.json");
                                        Console.Clear();
                                        Console.WriteLine("UPDATE STUDENT\n");
                                        Console.Write("Student's Address was updated to {0}. \n\nPress Enter to continue. . . ", studentToUpdate.Address.ToUpper());
                                        Console.ReadKey();
                                        loopOption = false;
                                        break;
                                    default:
                                        Invalid();
                                        break;
                                }
                            }
                            else if (option == 0)
                            {
                                loopOption = false;
                            }
                        }
                        catch
                        {
                            Invalid();
                        }
                    }
                }
                else
                {
                    Invalid();
                }
            }
            catch
            {
                Invalid();
            }
        }
    }

    public static sbyte GradesMenu()
    {
        try
        {
            Console.Clear();
            Console.Write("GRADES MENU \n\n1. Add Grade \n2. Remove Grade \n3. Update Grade \n\n0. Back to Main Menu \n\nOption: ");
            return Convert.ToSByte(Console.ReadLine());
        }
        catch
        {
            return 10;
        }
    }

    public static void AddGrade(List<Student> listStudents)
    {
        bool loop = true;
        while (loop)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("ADD GRADE\n");
                for (int i = 0; i < listStudents.Count; i++)
                {
                    Console.WriteLine("{0, -15} | {1, -40}", listStudents[i].Id, listStudents[i].Name);
                }
                Console.Write("\n0. Go back to Grades Menu \n\nId of the student to add a new grade: ");
                ulong idToAddGrade = ulong.Parse(Console.ReadLine());
                Student studentToAddGrade = listStudents.FirstOrDefault(student => student.Id.Equals(idToAddGrade));
                if (idToAddGrade == 0)
                {
                    loop = false;
                }
                else if (studentToAddGrade != null)
                {
                    bool loopOption = true;
                    while (loopOption)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("ADD GRADE");
                            Console.Write("\n1. Add quiz grade \n2. Add work grade \n3. Add assessment grade \n\n0. Go back to Grades Menu \n\nOption: ");
                            sbyte optionGrade = Convert.ToSByte(Console.ReadLine());
                            if (optionGrade > 0 && optionGrade <= 3)
                            {
                                Console.Clear();
                                Console.WriteLine("ADD GRADE");
                                Console.Write("\nGrade to add (1.0 to 5.0): ");
                                float newGrade = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                if (newGrade >= 1 && newGrade <= 5.0)
                                {
                                    switch (optionGrade)
                                    {
                                        case 1:
                                            studentToAddGrade.Quizzes.Add(newGrade);
                                            ExportCsharpListToJson(listStudents, "Student.json");
                                            Console.Clear();
                                            Console.WriteLine("ADD GRADE\n");
                                            Console.Write("Grade {0} Quiz was added. \n\nPress Enter to continue. . . ", newGrade);
                                            Console.ReadKey();
                                            loopOption = false;
                                            break;
                                        case 2:
                                            studentToAddGrade.Works.Add(newGrade);
                                            ExportCsharpListToJson(listStudents, "Student.json");
                                            Console.Clear();
                                            Console.WriteLine("ADD GRADE\n");
                                            Console.Write("Grade {0} Work was added. \n\nPress Enter to continue. . . ", newGrade);
                                            Console.ReadKey();
                                            loopOption = false;
                                            break;
                                        case 3:
                                            studentToAddGrade.Assessments.Add(newGrade);
                                            ExportCsharpListToJson(listStudents, "Student.json");
                                            Console.Clear();
                                            Console.WriteLine("ADD GRADE\n");
                                            Console.Write("Grade {0} Assessment was added. \n\nPress Enter to continue. . . ", newGrade);
                                            Console.ReadKey();
                                            loopOption = false;
                                            break;
                                        case 0:
                                            loopOption = false;
                                            break;
                                        default:
                                            Invalid();
                                            break;
                                    }
                                }
                                else
                                {
                                    Invalid();
                                }
                            }
                            else
                            {
                                Invalid();
                            }
                        }
                        catch
                        {
                            Invalid();
                        }
                    }
                }
                else
                {
                    Invalid();
                }
            }
            catch
            {
                Invalid();
            }
        }
    }

    public static void RemoveGrade(List<Student> listStudents)
    {
        bool loop = true;
        while (loop)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("REMOVE GRADE\n");
                foreach (Student student in listStudents)
                {
                    Console.WriteLine("{0, -15} | {1, -40}", student.Id, student.Name);
                }
                Console.Write("\n0. Go back to Grades Menu \n\nId of the student to remove: ");
                ulong idToRemoveGrade = ulong.Parse(Console.ReadLine());
                Student studentToRemoveGrade = listStudents.FirstOrDefault(student => student.Id.Equals(idToRemoveGrade));
                if (idToRemoveGrade == 0)
                {
                    loop = false;
                }
                else if (studentToRemoveGrade != null)
                {
                    bool loopRemove = true;
                    while (loopRemove)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("REMOVE GRADE\n");
                            Console.Write("1. Remove Quiz \n2. Remove Work \n3. Remove Assessment \n\n0. Go back \n\nOption: ");
                            sbyte optionToRemove = Convert.ToSByte(Console.ReadLine());
                            if (optionToRemove >= 1 && optionToRemove <= 3)
                            {
                                switch (optionToRemove)
                                {
                                    case 1:
                                        Console.Clear();
                                        Console.WriteLine("REMOVE GRADE\n");
                                        if (studentToRemoveGrade.Quizzes.Count == 0)
                                        {
                                            Console.WriteLine("There are no Quizzes to remove.");
                                        }
                                        else
                                        {
                                            for (int i = 0; i < studentToRemoveGrade.Quizzes.Count; i++)
                                            {
                                                Console.WriteLine($"Quiz {i + 1}: {studentToRemoveGrade.Quizzes[i]}");
                                            }
                                        }
                                        Console.Write("\n0. Go back \n\nQuiz to remove: ");
                                        int quizToRemove = Convert.ToInt32(Console.ReadLine());
                                        if (quizToRemove > 0 && quizToRemove <= studentToRemoveGrade.Quizzes.Count)
                                        {
                                            studentToRemoveGrade.Quizzes.Remove(studentToRemoveGrade.Quizzes[quizToRemove - 1]);
                                            ExportCsharpListToJson(listStudents, "Student.json");
                                            Console.Clear();
                                            Console.Write($"Quiz {quizToRemove} was removed. \n\nPress Enter to continue. . . ");
                                            Console.ReadKey();
                                        }
                                        break;
                                    case 2:
                                        Console.Clear();
                                        Console.WriteLine("REMOVE GRADE\n");
                                        if (studentToRemoveGrade.Works.Count == 0)
                                        {
                                            Console.WriteLine("There are no Works to remove.");
                                        }
                                        else
                                        {
                                            for (int i = 0; i < studentToRemoveGrade.Works.Count; i++)
                                            {
                                                Console.WriteLine($"Quiz {i + 1}: {studentToRemoveGrade.Works[i]}");
                                            }
                                        }
                                        Console.Write("\n0. Go back \n\nWork to remove: ");
                                        int workToRemove = Convert.ToInt32(Console.ReadLine());
                                        if (workToRemove > 0 && workToRemove <= studentToRemoveGrade.Works.Count)
                                        {
                                            studentToRemoveGrade.Works.Remove(studentToRemoveGrade.Works[workToRemove - 1]);
                                            ExportCsharpListToJson(listStudents, "Student.json");
                                            Console.Clear();
                                            Console.Write($"Work {workToRemove} was removed. \n\nPress Enter to continue. . . ");
                                            Console.ReadKey();
                                        }
                                        break;
                                    case 3:
                                        Console.Clear();
                                        Console.WriteLine("REMOVE GRADE\n");
                                        Console.Clear();
                                        Console.WriteLine("REMOVE GRADE\n");
                                        if (studentToRemoveGrade.Assessments.Count == 0)
                                        {
                                            Console.WriteLine("There are no Assessments to remove.");
                                        }
                                        else
                                        {
                                            for (int i = 0; i < studentToRemoveGrade.Assessments.Count; i++)
                                            {
                                                Console.WriteLine($"Quiz {i + 1}: {studentToRemoveGrade.Assessments[i]}");
                                            }
                                        }
                                        Console.Write("\n0. Go back \n\nAssessment to remove: ");
                                        int assessmentToRemove = Convert.ToInt32(Console.ReadLine());
                                        if (assessmentToRemove > 0 && assessmentToRemove <= studentToRemoveGrade.Assessments.Count)
                                        {
                                            studentToRemoveGrade.Assessments.Remove(studentToRemoveGrade.Assessments[assessmentToRemove - 1]);
                                            ExportCsharpListToJson(listStudents, "Student.json");
                                            Console.Clear();
                                            Console.Write($"Assessment {assessmentToRemove} was removed. \n\nPress Enter to continue. . . ");
                                            Console.ReadKey();
                                        }
                                        break;
                                }
                            }
                            else if (optionToRemove == 0)
                            {
                                loopRemove = false;
                            }
                            else
                            {
                                Invalid();
                            }
                        }
                        catch
                        {
                            Invalid();
                        }
                    }
                }
                else
                {
                    Invalid();
                }
            }
            catch
            {
                Invalid();
            }
        }
    }

    public static void UpdateGrade(List<Student> listStudents)
    {
        bool loop = true;
        while (loop)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("UPDATE GRADE\n");
                foreach (Student student in listStudents)
                {
                    Console.WriteLine("{0, -15} | {1, -40}", student.Id, student.Name);
                }
                Console.Write("\n0. Go back to Grades Menu \n\nId of the student to update: ");
                ulong idToUpdateGrade = ulong.Parse(Console.ReadLine());
                Student studentToUpdateGrade = listStudents.FirstOrDefault(student => student.Id.Equals(idToUpdateGrade));
                if (idToUpdateGrade == 0)
                {
                    loop = false;
                }
                else if (studentToUpdateGrade != null)
                {
                    bool loopUpdate = true;
                    while (loopUpdate)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("UPDATE GRADE\n");
                            Console.Write("1. Update Quiz \n2. Update Work \n3. Update Assessment \n\n0. Go back \n\nOption: ");
                            sbyte optionToUpdate = Convert.ToSByte(Console.ReadLine());
                            if (optionToUpdate >= 1 && optionToUpdate <= 3)
                            {
                                switch (optionToUpdate)
                                {
                                    case 1:
                                        Console.Clear();
                                        Console.WriteLine("UPDATE GRADE\n");
                                        if (studentToUpdateGrade.Quizzes.Count == 0)
                                        {
                                            Console.WriteLine("There are no Quizzes to update.");
                                        }
                                        else
                                        {
                                            for (int i = 0; i < studentToUpdateGrade.Quizzes.Count; i++)
                                            {
                                                Console.WriteLine($"Quiz {i + 1}: {studentToUpdateGrade.Quizzes[i]}");
                                            }
                                        }
                                        Console.Write("\n0. Go back \n\nQuiz to update: ");
                                        int quizToUpdate = Convert.ToInt32(Console.ReadLine());
                                        if (quizToUpdate > 0 && quizToUpdate <= studentToUpdateGrade.Quizzes.Count)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("UPDATE GRADE");
                                            Console.WriteLine("\nCurrent grade: " + studentToUpdateGrade.Quizzes[quizToUpdate - 1]);
                                            Console.Write("\nNew grade (1.0 - 5.0): ");
                                            float newGrade = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                            if (newGrade >= 1 && newGrade <= 5.0)
                                            {
                                                studentToUpdateGrade.Quizzes[quizToUpdate - 1] = newGrade;
                                                ExportCsharpListToJson(listStudents, "Student.json");
                                                Console.Clear();
                                                Console.Write($"Quiz {quizToUpdate} was updated to {newGrade}. \n\nPress Enter to continue. . . ");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Invalid();
                                            }
                                        }
                                        break;
                                    case 2:
                                        Console.Clear();
                                        Console.WriteLine("UPDATE GRADE\n");
                                        if (studentToUpdateGrade.Works.Count == 0)
                                        {
                                            Console.WriteLine("There are no Works to update.");
                                        }
                                        else
                                        {
                                            for (int i = 0; i < studentToUpdateGrade.Works.Count; i++)
                                            {
                                                Console.WriteLine($"Work {i + 1}: {studentToUpdateGrade.Works[i]}");
                                            }
                                        }
                                        Console.Write("\n0. Go back \n\nWork to update: ");
                                        int workToUpdate = Convert.ToInt32(Console.ReadLine());
                                        if (workToUpdate > 0 && workToUpdate <= studentToUpdateGrade.Works.Count)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("UPDATE GRADE");
                                            Console.WriteLine("\nCurrent grade: " + studentToUpdateGrade.Works[workToUpdate - 1]);
                                            Console.Write("\nNew grade (1.0 - 5.0): ");
                                            float newGrade = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                            if (newGrade >= 1 && newGrade <= 5.0)
                                            {
                                                studentToUpdateGrade.Works[workToUpdate - 1] = newGrade;
                                                ExportCsharpListToJson(listStudents, "Student.json");
                                                Console.Clear();
                                                Console.Write($"Work {workToUpdate} was updated to {newGrade}. \n\nPress Enter to continue. . . ");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Invalid();
                                            }
                                        }
                                        break;
                                    case 3:
                                        Console.Clear();
                                        Console.WriteLine("UPDATE GRADE\n");
                                        if (studentToUpdateGrade.Assessments.Count == 0)
                                        {
                                            Console.WriteLine("There are no Assessments to update.");
                                        }
                                        else
                                        {
                                            for (int i = 0; i < studentToUpdateGrade.Assessments.Count; i++)
                                            {
                                                Console.WriteLine($"Assessment {i + 1}: {studentToUpdateGrade.Assessments[i]}");
                                            }
                                        }
                                        Console.Write("\n0. Go back \n\nAssessment to update: ");
                                        int assessmentToUpdate = Convert.ToInt32(Console.ReadLine());
                                        if (assessmentToUpdate > 0 && assessmentToUpdate <= studentToUpdateGrade.Assessments.Count)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("UPDATE GRADE");
                                            Console.WriteLine("\nCurrent grade: " + studentToUpdateGrade.Assessments[assessmentToUpdate - 1]);
                                            Console.Write("\nNew grade (1.0 - 5.0): ");
                                            float newGrade = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                            if (newGrade >= 1 && newGrade <= 5.0)
                                            {
                                                studentToUpdateGrade.Assessments[assessmentToUpdate - 1] = newGrade;
                                                ExportCsharpListToJson(listStudents, "Student.json");
                                                Console.Clear();
                                                Console.Write($"Assessment {assessmentToUpdate} was updated to {newGrade}. \n\nPress Enter to continue. . . ");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Invalid();
                                            }
                                        }
                                        break;
                                }
                            }
                            else if (optionToUpdate == 0)
                            {
                                loopUpdate = false;
                            }
                            else
                            {
                                Invalid();
                            }
                        }
                        catch
                        {
                            Invalid();
                        }
                    }
                }
                else
                {
                    Invalid();
                }
            }
            catch
            {
                Invalid();
            }
        }
    }

    public static sbyte GraphicsMenu()
    {
        try
        {
            Console.Clear();
            Console.Write("GRAPHICS MENU \n\n1. Student's Information \n2. General Grades \n3. Final Grades \n\n0. Back to Main Menu \n\nOption: ");
            return Convert.ToSByte(Console.ReadLine());
        }
        catch
        {
            return 10;
        }
    }

    public static void StudentsInformationGraphic(List<Student> listStudents)
    {
        Console.Clear();
        Console.WriteLine("+-----------------+------------------------------------------+------------------------------------------+------+--------------------------------+");
        Console.WriteLine("| {0,-15} | {1,-40} | {2,-40} | {3,-4} | {4,-30} |", "ID", "NAME", "EMAIL", "AGE", "ADDRESS");
        for (int i = 0; i < listStudents.Count; i++)
        {
            string iString = i.ToString();
            char lastDigit = iString[iString.Length - 1];
            Console.WriteLine("+-----------------+------------------------------------------+------------------------------------------+------+--------------------------------+");
            Console.WriteLine("| {0,-15} | {1,-40} | {2,-40} | {3,-4} | {4,-30} |", listStudents[i].Id, listStudents[i].Name, listStudents[i].Email, listStudents[i].Age, listStudents[i].Address);
            if (lastDigit == '9')
            {
                Console.WriteLine("+-----------------+------------------------------------------+------------------------------------------+------+--------------------------------+");
                Console.Write("\nPress Enter to continue. . . ");
                Console.ReadKey();
            }
        }
        Console.WriteLine("+-----------------+------------------------------------------+------------------------------------------+------+--------------------------------+");
        Console.Write("\nPress Enter to continue. . . ");
        Console.ReadKey();
        Console.Clear();
    }

    public static void GeneralGradesGraphic(List<Student> listStudents)
    {
        bool loop = true;
        while (loop)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("SHOW GENERAL GRADES\n");
                foreach (Student student in listStudents)
                {
                    Console.WriteLine("{0, -15} | {1, -40}", student.Id, student.Name);
                }
                Console.Write("\n0. Go back to Graphics Menu \n\nId of the student to show grades: ");
                ulong idToShowGrade = ulong.Parse(Console.ReadLine());
                Student studentToShowGrade = listStudents.FirstOrDefault(student => student.Id.Equals(idToShowGrade));
                if (idToShowGrade == 0)
                {
                    loop = false;
                }
                else if (studentToShowGrade != null)
                {
                    bool loopShow = true;
                    while (loopShow)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("SHOW GENERAL GRADES\n");
                            Console.Write("1. Show Quizzes \n2. Show Works \n3. Show Assessments \n\n0. Go back \n\nOption: ");
                            sbyte optionToShow = Convert.ToSByte(Console.ReadLine());
                            if (optionToShow >= 1 && optionToShow <= 3)
                            {
                                switch (optionToShow)
                                {
                                    case 1:
                                        Console.Clear();
                                        Console.WriteLine("SHOW GENERAL GRADES\n");
                                        if (studentToShowGrade.Quizzes.Count == 0)
                                        {
                                            Console.WriteLine("There are no Quizzes to show.");
                                        }
                                        else
                                        {
                                            for (int i = 0; i < studentToShowGrade.Quizzes.Count; i++)
                                            {
                                                Console.WriteLine($"Quiz {i + 1}: {studentToShowGrade.Quizzes[i]}");
                                            }
                                        }
                                        Console.Write("\nPress Enter to continue. . . ");
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        Console.Clear();
                                        Console.WriteLine("SHOW GENERAL GRADES\n");
                                        if (studentToShowGrade.Works.Count == 0)
                                        {
                                            Console.WriteLine("There are no Works to show.");
                                        }
                                        else
                                        {
                                            for (int i = 0; i < studentToShowGrade.Works.Count; i++)
                                            {
                                                Console.WriteLine($"Quiz {i + 1}: {studentToShowGrade.Works[i]}");
                                            }
                                        }
                                        Console.Write("\nPress Enter to continue. . . ");
                                        Console.ReadKey();
                                        break;
                                    case 3:
                                        Console.Clear();
                                        Console.WriteLine("SHOW GENERAL GRADES\n");
                                        if (studentToShowGrade.Assessments.Count == 0)
                                        {
                                            Console.WriteLine("There are no Assessments to show.");
                                        }
                                        else
                                        {
                                            for (int i = 0; i < studentToShowGrade.Assessments.Count; i++)
                                            {
                                                Console.WriteLine($"Quiz {i + 1}: {studentToShowGrade.Assessments[i]}");
                                            }
                                        }
                                        Console.Write("\nPress Enter to continue. . . ");
                                        Console.ReadKey();
                                        break;
                                }
                            }
                            else if (optionToShow == 0)
                            {
                                loopShow = false;
                            }
                            else
                            {
                                Invalid();
                            }
                        }
                        catch
                        {
                            Invalid();
                        }
                    }
                }
                else
                {
                    Invalid();
                }
            }
            catch
            {
                Invalid();
            }
        }
    }

    public static void FinalGradesGraphic(List<Student> listStudents)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("+-----------------+------------------------------------------+--------------------------------------------+-------+");
            Console.WriteLine("| {0,-15} | {1,-40} | {2,-10} | {3,-10} | {4,-16} | {5,-5} |", "ID", "NAME", "FINAL QUIZ", "FINAL WORK", "FINAL ASSESSMENT", "FINAL");
            for (int i = 0; i < listStudents.Count; i++)
            {
                float addingQuizzes = 0;
                for (int j = 0; j < listStudents[i].Quizzes.Count; j++)
                {
                    try
                    {
                        addingQuizzes += listStudents[i].Quizzes[j];
                    }
                    catch
                    {
                        addingQuizzes += 0;
                    }
                }
                float addingWorks = 0;
                for (int j = 0; j < listStudents[i].Works.Count; j++)
                {
                    try
                    {
                        addingWorks += listStudents[i].Works[j];
                    }
                    catch
                    {
                        addingWorks += 0;
                    }
                }
                float addingAssessments = 0;
                for (int j = 0; j < listStudents[i].Assessments.Count; j++)
                {
                    try
                    {
                        addingAssessments += listStudents[i].Assessments[j];
                    }
                    catch
                    {
                        addingAssessments += 0;
                    }
                }
                float finalQuizzes;
                if (listStudents[i].Quizzes.Count != 0)
                {
                    finalQuizzes = addingQuizzes / listStudents[i].Quizzes.Count * 0.25f;
                }
                else
                {
                    finalQuizzes = 0;
                }
                float finalWorks;
                if (listStudents[i].Works.Count != 0)
                {
                    finalWorks = addingWorks / listStudents[i].Works.Count * 0.60f;
                }
                else
                {
                    finalWorks = 0;
                }
                float finalAssessments;
                if (listStudents[i].Assessments.Count != 0)
                {
                    finalAssessments = addingAssessments / listStudents[i].Assessments.Count * 0.15f;
                }
                else
                {
                    finalAssessments = 0;
                }
                float final = finalQuizzes + finalWorks + finalAssessments;
                string iString = i.ToString();
                char lastDigit = iString[iString.Length - 1];
                Console.WriteLine("+-----------------+------------------------------------------+------------+------------+------------------+-------+");
                Console.WriteLine("| {0,-15} | {1,-40} | {2,-10} | {3,-10} | {4,-16} | {5,-5} |", listStudents[i].Id, listStudents[i].Name, finalQuizzes, finalWorks, finalAssessments, final);
                if (lastDigit == '9')
                {
                    Console.WriteLine("+-----------------+------------------------------------------+------------+------------+------------------+-------+");
                    Console.Write("\nPress Enter to continue. . . ");
                    Console.ReadKey();
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("+-----------------+------------------------------------------+------------+------------+------------------+-------+");
            Console.Write("\nPress Enter to continue. . . ");
            Console.ReadKey();
            Console.Clear();
        }
        catch
        {
            Console.WriteLine("xd");
            Console.ReadKey();
        }
    }
}