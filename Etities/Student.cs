namespace pruebaJson.Etities;

public class Student : Grades
{
    private ulong id;
    private string name;
    private string email;
    private sbyte age;
    private string address;

    public Student() : base()
    {
    }
    public Student(ulong id, string name, string email, sbyte age, string address, List<float> quizzes, List<float> works, List<float> assessments) : base(quizzes, works, assessments)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.Age = age;
        this.Address = address;
        this.Quizzes = quizzes;
        this.Works = works;
        this.Assessments = assessments;
    }

    public ulong Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Email { get => email; set => email = value; }
    public sbyte Age { get => age; set => age = value; }
    public string Address { get => address; set => address = value; }

    internal static Student FirstOrDefault(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }
}