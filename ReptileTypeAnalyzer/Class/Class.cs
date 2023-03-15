using System.ComponentModel;

[Flags]
enum Reptile
{
    BlackMamba = 1,
    CottonMouth = 2,
    Wiper = 3,
    Crocodile = 10,
    Aligator = 11,
    HawksbillTurtle = 30,
    LoggerheadTurtle = 39,
    Cat = 40,
    [Description("Is a unknown Reptile")]
    Unknown
}

public enum ReptileType
{
    [Description("Snake")]
    Snake,
    [Description("Lizard")]
    Lizard,
    [Description("Turtle")]
    Turtle,
    [Description("Is not a Reptile")]
    Invalid,
}