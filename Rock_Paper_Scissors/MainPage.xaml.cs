using Java.Util.Jar;
using Image = Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific.Image;

namespace Rock_Paper_Scissors;

public partial class MainPage : ContentPage
{
    private int _playerScore;
    private int _systemScore;
    private int rock = 2;
    private int paper = 1;
    private int scissors = 3;
    
    public MainPage()
    {
        InitializeComponent();
    }

    // When user click their chose this function handles changing the player image to theri chose
    void onImageButtonClick(object sender, EventArgs e)
    {
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
        
    }
    
}