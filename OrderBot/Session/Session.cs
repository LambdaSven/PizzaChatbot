namespace PizzaBot.Sessions
{
  public class Session
  {
    private string id;
    private bool beenGreeted;
    public bool BeenGreeted 
    { 
      get
      {
        return beenGreeted;
      } 
      set 
      {
        beenGreeted = value;
      } 
    }   
    public Session(string id)
    {
      this.id = id;
      this.beenGreeted = false;
    }
  }
}