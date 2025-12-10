// See https://aka.ms/new-console-template for more information
using orichi_dotnet_demo.Extensions;

Console.Write("Number of person to init: ");
var numOfPerson = Convert.ToInt32(Console.ReadLine());
Person[] persons = new Person[numOfPerson];
for (int i = 0; i < numOfPerson; i++)
{
    persons[i] = new Person($"Nguyen Van {Convert.ToChar(65 + i)}",
        new Score(
            new Random().Next(3, 11),
            new Random().Next(3, 11),
            new Random().Next(3, 11)
        ));
}

Console.WriteLine("Person list");
foreach (var person in persons)
{
    Console.WriteLine($"{person.Name} " +
        $"- Math:{person.Scores.Math} " +
        $"Physic:{person.Scores.Physic} " +
        $"Chemistry:{person.Scores.Chemistry} " +
        $"Avg:{person.Scores.AverageNumericProperties()}");
}

BubbleSort(persons);

Console.WriteLine("");
Console.WriteLine("Person list after sorting");
foreach (var person in persons)
{
    Console.WriteLine($"{person.Name} " +
        $"- Math:{person.Scores.Math} " +
        $"Physic:{person.Scores.Physic} " +
        $"Chemistry:{person.Scores.Chemistry} " +
        $"Avg:{person.Scores.AverageNumericProperties()}");
}

Console.WriteLine("");
Console.Write("AvgPoint want to search: ");
var searchPoint = Convert.ToInt32(Console.ReadLine());
var foundPerson = BinarySearch(persons, searchPoint);
if (foundPerson != null)
{
    Console.WriteLine($"{foundPerson.Name} " +
        $"- Math:{foundPerson.Scores.Math} " +
        $"Physic:{foundPerson.Scores.Physic} " +
        $"Chemistry:{foundPerson.Scores.Chemistry} " +
        $"Avg:{foundPerson.Scores.AverageNumericProperties()}");
    Console.ReadLine();
}
else
{
    Console.WriteLine($"Cannot find any person with AvgPoint is : {searchPoint}");
    Console.ReadLine();
}

#region private function
static void BubbleSort(Person[] array)
{
    int n = array.Length;
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - i - 1; j++)
        {
            var thisPersonAvgPoint = array[j].AvgScore;
            var nextPersonAvgPoint = array[j + 1].AvgScore;
            // Compare based on Person average points 
            if (thisPersonAvgPoint < nextPersonAvgPoint)
            {
                var temp = array[j];
                array[j] = array[j + 1];
                array[j + 1] = temp;
            }
            else if (thisPersonAvgPoint == nextPersonAvgPoint)
            {
                // If average points keys are equal, compare based on name
                if (string.Compare(array[j].Name, array[j + 1].Name) > 0)
                {
                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
}

static Person BinarySearch(Person[] array, int avgPoint)
{
    int left = 0;
    int right = array.Length - 1;
    int steps = 1;

    while (left <= right)
    {
        int mid = left + (right - left) / 2;
        if (array[mid].AvgScore == avgPoint)
        {
            Console.WriteLine($"Found a person with AvgPoint is: {avgPoint} after {steps} steps");
            return array[mid];
        }
        else if (array[mid].AvgScore > avgPoint)
        {
            left = mid + 1;
        }
        else
        {
            right = mid - 1;
        }
        steps++;
    }

    return null;
}

#endregion private function

#region class object
class Person
{
    public Person(string name, Score scores)
    {
        Name = name;
        Scores = scores;
        AvgScore = Scores.AverageNumericProperties();
    }

    public string Name { get; }
    public Score Scores { get; }
    public int AvgScore { get; }
}

class Score {
    public Score(int math, int physic, int chemistry)
    {
        Math = math;
        Physic = physic;
        Chemistry = chemistry;
    }

    public int Math { get;}
    public int Physic { get;  }
    public int Chemistry { get; }
}
#endregion class object