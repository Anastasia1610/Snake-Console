//ConsoleKeyInfo key = Console.ReadKey();

//if (key.Key == ConsoleKey.Enter)
//{
//    Console.WriteLine("It's Enter!");
//}


//Console.WriteLine("Welcome to the Snake game!\n\nTo continue, type the SIZE of the playing field and press Enter...");
//int size = 20;
//if(int.TryParse(Console.ReadLine(), out int result))
//    if(result >= 10)
//        size = result;




using System.Drawing;

Game myGame = new Game();
myGame.FieldGeneration();
myGame.AddSnake(new Snake());
Console.WriteLine("Welcome to the Snake game!\n\nTo continue press Enter...");
Console.ReadLine();


do
{
    Thread.Sleep(100);
    Console.Clear();
    myGame.ShowField();
    // свич с направление движения
    //if (myGame.Direction == Direction.Start) continue;
    //else if(myGame.Direction == Direction.Right) 


    //ConsoleKeyInfo key = Console.ReadKey();
   
    switch(Console.ReadKey(true).Key)
    {
        case ConsoleKey.A:
            myGame.Direction = Direction.Left;  
            break;
        case ConsoleKey.D:  
            myGame.Direction = Direction.Right; 
            break;
        case ConsoleKey.W:
            myGame.Direction = Direction.Up;
            break;
        case ConsoleKey.S:
            myGame.Direction = Direction.Down;
            break;
        default:
            break;
    }
    myGame.Moving();
    myGame.AddSnake(myGame.Player);
} while (true);





class Game
{
    public Snake Player { get; set; } = new Snake();
    public List<List<string>> Field { get; set; } = new List<List<string>>();

    public Direction Direction { get; set; }   

    public void FieldGeneration()
    {
        int size = 20;
        for (int i = 0; i < size; i++)
        {
            Field.Add(new List<string>());
            for (int j = 0; j < size; j++)
            {
                if (i == 0 || i == size - 1 || j == 0 || j == size - 1)
                    Field[i].Add("X");
                else
                    Field[i].Add(" ");
            }
        }
    }

    public void ShowField()
    {
        foreach (var item in Field)
        {
            foreach (var el in item)
                Console.Write(el);
            Console.WriteLine();
        }
    }

    public void AddSnake(Snake snake)
    {
        for (int i = 0; i < Field.Count(); i++)
        {
            for (int j = 0; j < Field[i].Count(); j++)
            {
                if (snake.Body[0].X == i && snake.Body[0].Y == j)
                    Field[i][j] = snake.Body[0].Symbol;
            }
        }
    }

    public void Moving()
    {
        switch (Direction)
        {
            case Direction.Up:
                Player.Body[0].X -= 1;
                break;
            case Direction.Down:
                break;
            case Direction.Left:
                break;
            case Direction.Right:
                break;
            default:
                break;
        }
    }
}

class Snake
{
    public Snake(int firstXCoordinate = 5, int firstYCoordinate = 5, List<BodyElement> body = null)
    {
        if (body != null)
            Body = body;
        else
        {
            Body = new List<BodyElement>();
            Body.Add(new Head(firstXCoordinate, firstYCoordinate, "■"));
        }
    }

    public List<BodyElement> Body { get; set; }
}

class Head : BodyElement
{
    public Head(int x, int y, string symbol) : base(x,y, symbol)
    {
    }
}

class BodyElement
{
    public BodyElement(int x, int y, string symbol)
    {
        X = x;
        Y = y;
        Symbol = symbol;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public string Symbol { get; set; } = "○";
}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}
