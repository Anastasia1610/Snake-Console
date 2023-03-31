//ConsoleKeyInfo key = Console.ReadKey();

//if (key.Key == ConsoleKey.Enter)
//{
//    Console.WriteLine("It's Enter!");
//}



Game myGame = new Game(Up, Down, Left, Right);
myGame.Losing += Loss;
myGame.Player.EatFoodHandler += Eat;
myGame.GameOver += Clearing;
myGame.GameOver += MessageOfGameOver;
myGame.GameOver += PointsInfo;

myGame.FieldGeneration();
myGame.AddSnake(new Snake());
myGame.FoodGeneration();
myGame.AddFood();
bool end;


//Console.WriteLine("Welcome to the Snake game!\n\nTo continue press Enter...");
//Console.ReadLine();

do
{
    Thread.Sleep(100);
    Console.Clear();
    PointsInfo(myGame);
    myGame.ShowField();

    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.W:
            myGame.Buttons[0].Clicking(myGame);
            break;
        case ConsoleKey.S:
            myGame.Buttons[1].Clicking(myGame);
            break;
        case ConsoleKey.A:
            myGame.Buttons[2].Clicking(myGame);
            break;
        case ConsoleKey.D:
            myGame.Buttons[3].Clicking(myGame);
            break;
        default:
            break;
    }

    myGame.AddSnake(myGame.Player);

    if(myGame.Player.EatingFood(myGame))
    {
        myGame.Points++;
        myGame.FoodGeneration();
    }
       
    myGame.AddFood();
    end = myGame.Lose(myGame);
        
} while (end);
 






// МЕТОДЫ

bool Loss(object sender)
{
    Game myGame = sender as Game;
    //Столкновение с границами
    if (myGame.Player.Body[0].X == 0 || myGame.Player.Body[0].Y == 0 || myGame.Player.Body[0].X == myGame.Field.Count() - 1 || myGame.Player.Body[0].Y == myGame.Field.Count() - 1)
        return false;
       
    //Столкновение со своим телом
    for (int i = 1; i < myGame.Player.Body.Count(); i++)
        if (myGame.Player.Body[0].X == myGame.Player.Body[i].X && myGame.Player.Body[0].Y == myGame.Player.Body[i].Y)
            return false;
    return true;
}


void Up(object sender)
{
    Game myGame = sender as Game;

    //Создание дополнительного элемента тела змеи 
    BodyElement newBodyElement = new BodyElement(myGame.Player.Body[myGame.Player.Body.Count() - 1].X,
            myGame.Player.Body[myGame.Player.Body.Count() - 1].Y, "O");
   
    //Перезаписывание координат элементов тела змеи
    for (int i = myGame.Player.Body.Count()-1; i >= 1; i--)
    {
        myGame.Player.Body[i].X = myGame.Player.Body[i - 1].X;
        myGame.Player.Body[i].Y = myGame.Player.Body[i - 1].Y;
    }

    //Изменение координат головы
    myGame.Player.Body[0].X--;

    //если съела еду - добавление дополнительного элемента тела змеи
    if (myGame.Player.EatingFood(sender))
        myGame.Player.Body.Add(newBodyElement);
}

void Down(object sender)
{
    Game myGame = sender as Game;

    //Создание дополнительного элемента тела змеи 
    BodyElement newBodyElement = new BodyElement(myGame.Player.Body[myGame.Player.Body.Count() - 1].X,
            myGame.Player.Body[myGame.Player.Body.Count() - 1].Y, "O");

    //Перезаписывание координат элементов тела змеи
    for (int i = myGame.Player.Body.Count() - 1; i >= 1; i--)
    {
        myGame.Player.Body[i].X = myGame.Player.Body[i - 1].X;
        myGame.Player.Body[i].Y = myGame.Player.Body[i - 1].Y;
    }

    //Изменение координат головы
    myGame.Player.Body[0].X++;

    //если съела еду - добавление дополнительного элемента тела змеи
    if (myGame.Player.EatingFood(sender))
        myGame.Player.Body.Add(newBodyElement);
}

void Left(object sender)
{
    Game myGame = sender as Game;

    //Создание дополнительного элемента тела змеи 
    BodyElement newBodyElement = new BodyElement(myGame.Player.Body[myGame.Player.Body.Count() - 1].X,
            myGame.Player.Body[myGame.Player.Body.Count() - 1].Y, "O");

    //Перезаписывание координат элементов тела змеи
    for (int i = myGame.Player.Body.Count() - 1; i >= 1; i--)
    {
        myGame.Player.Body[i].X = myGame.Player.Body[i - 1].X;
        myGame.Player.Body[i].Y = myGame.Player.Body[i - 1].Y;
    }

    //Изменение координат головы
    myGame.Player.Body[0].Y--;

    //если съела еду - добавление дополнительного элемента тела змеи
    if (myGame.Player.EatingFood(sender))
        myGame.Player.Body.Add(newBodyElement);
}

void Right(object sender)
{
    Game myGame = sender as Game;

    //Создание дополнительного элемента тела змеи 
    BodyElement newBodyElement = new BodyElement(myGame.Player.Body[myGame.Player.Body.Count() - 1].X,
            myGame.Player.Body[myGame.Player.Body.Count() - 1].Y, "O");

    //Перезаписывание координат элементов тела змеи
    for (int i = myGame.Player.Body.Count() - 1; i >= 1; i--)
    {
        myGame.Player.Body[i].X = myGame.Player.Body[i - 1].X;
        myGame.Player.Body[i].Y = myGame.Player.Body[i - 1].Y;
    }

    //Изменение координат головы
    myGame.Player.Body[0].Y++;

    //если съела еду - добавление дополнительного элемента тела змеи
    if (myGame.Player.EatingFood(sender))
        myGame.Player.Body.Add(newBodyElement);
}

bool Eat(object sender)
{
    Game myGame = sender as Game;
    if (myGame.Player.Body[0].X == myGame.Food.X && myGame.Player.Body[0].Y == myGame.Food.Y)
        return true;
    return false;
}

void MessageOfGameOver(object sender)
{
    Console.WriteLine("You LOSE...");
}

void PointsInfo(object sender)
{
    Game myGame = sender as Game;
    Console.WriteLine($"Points: {myGame.Points}");
}

void Clearing(object sender)
{
    Console.Clear();
}





// КЛАССЫ

class Game
{
    public Game(Click click1, Click click2, Click click3, Click click4)
    {
        Buttons[0].ClickHandler += click1;
        Buttons[1].ClickHandler += click2;
        Buttons[2].ClickHandler += click3;
        Buttons[3].ClickHandler += click4;
    }

    public Snake Player { get; set; } = new Snake();
    public List<List<string>> Field { get; set; } = new List<List<string>>();
    public BodyElement Food { get; set; }

    public List<Button> Buttons { get; set; } = new List<Button>() 
    { new Button(ConsoleKey.W), new Button(ConsoleKey.S), new Button(ConsoleKey.A), new Button(ConsoleKey.D)};

    public event Lose Losing;

    public int Points { get; set; } = 0;

    public event GameOverDelegate GameOver;

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
        //Удаление старого изображения змеи с поля
        for (int i = 1; i < Field.Count() - 1; i++)
            for (int j = 1; j < Field[i].Count() - 1; j++)
                Field[i][j] = " ";
        //Добавление нового изображения змеи на поле
        for (int i = 0; i < Field.Count(); i++)
            for (int j = 0; j < Field[i].Count(); j++)
                for (int k = 0; k < snake.Body.Count(); k++)
                    if (snake.Body[k].X == i && snake.Body[k].Y == j)
                        Field[i][j] = snake.Body[k].Symbol;
    }

    public void FoodGeneration()
    {
        Random random = new Random();
        int x, y;
        bool flag;
        do
        {
            flag = false;
            x = random.Next(1, Field.Count() - 1);
            y = random.Next(1, Field.Count() - 1);
            for (int i = 0; i < Player.Body.Count(); i++)
            {
                if (x == Player.Body[i].X && y == Player.Body[i].Y)
                    flag = true;
            }

        } while (flag);
        Food = new BodyElement(x, y, "$");
    }

    public void AddFood()
    {
        for (int i = 0; i < Field.Count(); i++)
            for (int j = 0; j < Field[i].Count(); j++)
                if (Food.X == i && Food.Y == j)
                    Field[i][j] = Food.Symbol;
    }

    public bool Lose(object sender)
    {
        if (!this.Losing(sender))
            GameOver(sender);
        return this.Losing(sender); 
    }
}


class Snake
{
    public Snake(int firstXCoordinate = 5, int firstYCoordinate = 10)
    {
        Body = new List<BodyElement>() { new Head(firstXCoordinate, firstYCoordinate, "@"),
                new BodyElement(firstXCoordinate, firstYCoordinate + 1, "О"),
                new BodyElement(firstXCoordinate, firstYCoordinate+2, "О")};
    }

    public event EatFood EatFoodHandler;

    public List<BodyElement> Body { get; set; }

    public bool EatingFood(object sender)
    {
        return EatFoodHandler(sender);
    }
}


class Head : BodyElement
{
    public Head(int x, int y, string symbol) : base(x, y, symbol)
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
    public string Symbol { get; set; }
}

class Button
{
    public Button(ConsoleKey key)
    {
        this.key = key;
    }

    public ConsoleKey key { get; set; } 

    public event Click ClickHandler;

    public void Clicking(object sender)
    {
        ClickHandler(sender);
    }
}


//ДЕЛЕГАТЫ

public delegate bool Lose(object sender);
public delegate void Click(object sender);
public delegate bool EatFood(object sender);
public delegate void GameOverDelegate(object sender);
