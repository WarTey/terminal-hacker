using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hacker : MonoBehaviour
{
    List<List<string>> passwords;
    const string menuHint = "You may type menu at any time.";

    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    int level;
    string password;

    // Start is called before the first frame update
    void Start()
    {
        InitPasswords();
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;

        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for the local library.");
        Terminal.WriteLine("Press 2 for the police station.");
        Terminal.WriteLine("Press 3 for NASA.");
        Terminal.WriteLine("Entre your selection:");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidNumber = input == "1" || input == "2" || input == "3";

        if (isValidNumber)
        {
            level = int.Parse(input);
            password = passwords[level - 1][Random.Range(0, passwords[level - 1].Count)];
            StartGame();
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level.");
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Sorry, try again.");
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;

        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    ________
   /       //
  /       //
 /______ //
(_______(/
                ");
                Terminal.WriteLine("Play again for a greeter challenge.");
                break;
            case 2:
                Terminal.WriteLine("You got the prison key!");
                Terminal.WriteLine(@"
 __
/0 \_______
\__/-=' = '
                ");
                Terminal.WriteLine("Play again for a greeter challenge.");
                break;
            case 3:
                Terminal.WriteLine("Welcome to NASA's internal system!");
                Terminal.WriteLine(@"
 _ __   __ _ ___  __ _
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___)\__,_|
                ");
                Terminal.WriteLine("Play again for a greeter challenge.");
                break;
            default:
                Debug.LogError("Invalid level");
                break;
        }
    }

    void StartGame()
    {
        currentScreen = Screen.Password;

        Terminal.ClearScreen();
        Terminal.WriteLine("Please enter your password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void InitPasswords()
    {
        passwords = new List<List<string>> {
            new List<string> { "books", "aisle", "shelf", "password", "font", "borrow" },
            new List<string> { "prisonner", "handcuffs", "holster", "uniform", "arrest" },
            new List<string> { "starfield", "telescope", "environment", "exploration", "astronauts" }
        };
    }
}
