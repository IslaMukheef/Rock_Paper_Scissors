using Java.Util.Jar;
using Image = Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific.Image;

namespace Rock_Paper_Scissors;

public partial class MainPage : ContentPage
{
    private int systemWinCount = 0;
    private int playerWinCount = 0;
    private int _playerScore;
    private int _systemScore;
    private int rock = 2;
    private int paper = 1;
    private int scissors = 3;
    private bool gameStarted= false;


    
    
    public MainPage()
    {
        InitializeComponent();
        pictureSwitcher(); // change the picture to different one till the player starts the game!

    }

    // When user click their chose this function handles changing the player image to theri chose
    void onImageButtonClick(object sender, EventArgs e)
    {
        gameStarted = true;
        var userChosen = (ImageButton)sender;
        
        if (userChosen == PaperClicked)
        {
            PlayerChose.Source = "paper.png";
            roundLogic(paper);
        }
        else if (userChosen == RockClicked)
        {
            PlayerChose.Source = "rock.png";
            roundLogic(rock);
        }
        else if (userChosen == ScissorsClicked )
        {
            PlayerChose.Source = "scissors.png";
            roundLogic(scissors);
        }
        
    }
// This function generates a random number for the system chose and updates the picture and return the random value
    int systemPicture()
    {
        int _systemChose;
        Random randomChose = new Random();
        _systemChose = randomChose.Next(1, 4);
        switch (_systemChose)
        {
            case 1:
                SystemChose.Source = "paper.png";
                break;
            case 2:
                SystemChose.Source = "rock.png";
                break;
            case 3:
                SystemChose.Source = "scissors.png";
                break;
        }
        return _systemChose;
    }
    void roundLogic(int userInput)
    {
        
        int systemChose = systemPicture();
        if (userInput == systemChose)
        {
            //TIE
        }
        // instead of writing 3 if statement using one would be way better as we just check the users input here against the system
        else if (userInput == rock && systemChose == scissors || userInput == scissors && systemChose == paper ||
            userInput == paper && systemChose == rock)
        {
            _playerScore++;
            PlayerScore.Text = $"Player Score: {_playerScore.ToString()}";
        }
        // if the above statement fails means system won and user lost
        else
        {
            _systemScore++;
            SystemScore.Text = $"System Score: {_systemScore.ToString()}";
        }
        GameOver();
    }
    
    //Ends the game when the player or the system reaches 3 points
    void GameOver()
    {
        if (_playerScore == 3)
        {
            DisplayAlertAsync("Game Over", "You Win!", "OK");
            NewGame.IsEnabled = true;
            RockClicked.IsEnabled = false;
            PaperClicked.IsEnabled = false;
            ScissorsClicked.IsEnabled = false;
            playerWinCount++;
            if (playerWinCount >= 2)
            {
                DisplayAlertAsync("Match is Over", "System Gives up", "OK");
                NewGame.IsEnabled=false;
            }
        }
        else if (_systemScore == 3)
        {
            DisplayAlertAsync("Game Over", "You Lose!", "OK");
            NewGame.IsEnabled = true;
            RockClicked.IsEnabled = false;
            PaperClicked.IsEnabled = false;
            ScissorsClicked.IsEnabled = false;
            systemWinCount++;
            if (systemWinCount >= 2)
            {
                DisplayAlertAsync("Match is Over", "Give up", "OK");
                NewGame.IsEnabled=false;
            }
        }
    }

    // Reset the game when player clicks on new game button
    void OnNewGameClick(object sender, EventArgs e)
    {
        gameStarted = false;
        pictureSwitcher();
        RockClicked.IsEnabled = true;
        PaperClicked.IsEnabled = true;
        ScissorsClicked.IsEnabled = true;
        NewGame.IsEnabled = false;
        _systemScore=0;
        SystemScore.Text = $"System Score: {_systemScore.ToString()}";
        _playerScore = 0;
        PlayerScore.Text = $"Player Score: {_playerScore.ToString()}";
    }

    
    //https://stackoverflow.com/questions/18804394/how-to-wait-in-loop-without-blocking-ui-in-c-sharp
    //https://www.c-sharpcorner.com/article/async-and-await-in-c-sharp/
    private async Task pictureSwitcher()
    {
            while (!gameStarted)
            {
                Random random = new Random();
                int player = random.Next(0,5);
                int system = random.Next(0,5);
                PlayerChose.Source = $"question_mark{player}.png";
                SystemChose.Source = $"question_mark{system}.png";
                await Task.Delay(1000);
                
            }
    }
    
}