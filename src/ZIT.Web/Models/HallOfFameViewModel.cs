namespace ZIT.Web.Models;

public class HallOfFameViewModel
{
    public string[] Contributors { get; set; }

    public HallOfFameViewModel()
    {
        Contributors = new []
        {
            "Krzysztof Zajączkowski",
            "Grzegorz Strelczuk"
        };
    }
}