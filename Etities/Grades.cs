namespace pruebaJson.Etities;

public class Grades
{
    private List<float> quizzes = new List<float>();
    private List<float> works = new List<float>();
    private List<float> assessments = new List<float>();

    public Grades()
    {
    }
    public Grades(List<float> quizzes, List<float> works, List<float> assessments)
    {
        this.Quizzes = quizzes;
        this.Works = works;
        this.Assessments = assessments;
    }

    public List<float> Quizzes { get => quizzes; set => quizzes = value; }
    public List<float> Works { get => works; set => works = value; }
    public List<float> Assessments { get => assessments; set => assessments = value; }
}