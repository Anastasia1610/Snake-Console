﻿//ConsoleKeyInfo key = Console.ReadKey();

//if (key.Key == ConsoleKey.Enter)
//{
//    Console.WriteLine("It's Enter!");
//}





using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;

Game myGame = new Game();
myGame.FieldGeneration();
myGame.AddSnake(new Snake());
myGame.FoodGeneration();
myGame.AddFood();
    
//Console.WriteLine("Welcome to the Snake game!\n\nTo continue press Enter...");
//Console.ReadLine();

do
{
    Thread.Sleep(100);
    Console.Clear();
    myGame.ShowField();

    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.W:
                myGame.Direction = Direction.Up;
            break;
        case ConsoleKey.S:
                myGame.Direction = Direction.Down;
            break;
        default:
            break;
    }

    myGame.AddSnake(myGame.Player);

    if (myGame.CheckFoodEating(myGame.Player.Body))
        myGame.FoodGeneration();
        
    myGame.AddFood();
} while (myGame.Loss());

Console.WriteLine("You LOSE...");



class Game
{
    public Snake Player { get; set; } = new Snake();
    public List<List<string>> Field { get; set; } = new List<List<string>>();
    public BodyElement Food { get; set; }


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

    public bool CheckFoodEating(List<BodyElement> body)
    {
        if (body[0].X == Food.X && body[0].Y == Food.Y)
            return true;
        return false;
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

    public Snake(List<BodyElement> body)
    {
        Body = body;
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

enum Direction
{
    Up,
    Down,
    Left,
    Right
}
