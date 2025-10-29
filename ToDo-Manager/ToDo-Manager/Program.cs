

using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

public class ToDo
{
    public string Title { get; set; }
    public string Beschreibung { get; set; }
}

class Program
{

    static string jsonPath = "Todos.json";
    static List<ToDo> Todos = new List<ToDo>();
    static void Main()
    {
        LoadTodos();


        //Console.WriteLine("Adding Todo");
        //AddToDo("Test123", "Test");


        //Console.WriteLine("Updating Todo");
        //UpdateToDo("Test123", "Test1", "Test");


        Console.WriteLine("Removing Todo");
        RemoveToDo("Test1");

        SaveToDos();
    }

    public static void AddToDo(string Title, string Beschreibung)
    {
        if(IsTitleInList(Title))
        {
            Console.WriteLine("Title gibt es schon!");
            return;
        }

        Todos.Add(new ToDo { Title = Title,Beschreibung = Beschreibung });
    }

    public static void UpdateToDo(string AlterTitle, string NeuerTitle, string NeueBeschreibung)
    {

        if(IsTitleInList(NeuerTitle))
        {
            Console.WriteLine("Neuer Title gibt es schon!");
            return;
        }

        int index = getIndexOfTitle(AlterTitle);

        Todos[index] = new ToDo { 
                Title = NeuerTitle,
                Beschreibung = NeueBeschreibung
        };
    }
    public static void RemoveToDo(string Title)
    {

        if (!IsTitleInList(Title))
        {
            Console.WriteLine("Title gibt es nicht!");
            return;
        }

        int index = getIndexOfTitle(Title);
        Todos.RemoveAt(index);
    }


    public static void LoadTodos()
    {
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine("No Fiele Found");
            var EmptyList = new List<ToDo>();
            string json = JsonSerializer.Serialize(EmptyList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);
            Todos = EmptyList;
            Console.WriteLine("Created File");
            return;
        }

        string TodosJson = File.ReadAllText(jsonPath); 
        Todos = JsonSerializer.Deserialize<List<ToDo>>(TodosJson) ?? new List<ToDo>();
        Console.WriteLine("Todos Initzilased");
    }

    public static void SaveToDos()
    {
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine("No Fiele Found");
            var EmptyList = new List<ToDo>();
            string Emtyjson = JsonSerializer.Serialize(EmptyList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, Emtyjson);
            Console.WriteLine("Created File");
            return;
        }

        string Fulljson = JsonSerializer.Serialize(Todos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath,Fulljson); 

        Console.WriteLine("ToDos saved");
    }

    public static bool IsTitleInList(string Title)
    {
        for (int i = 0; i < Todos.Count; i++)
        {
            if (Todos[i].Title == Title)
            {
                return true ;
            }
        }
        return false ;
    }

    public static int getIndexOfTitle(string Title)
    {
        for (int i = 0; i < Todos.Count; i++)
        {
            if (Todos[i].Title == Title)
            {
                return i;
            }
        }

        return -1;
    }
}
