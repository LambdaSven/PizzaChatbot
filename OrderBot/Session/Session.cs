namespace PizzaBot.Sessions
{
  public class Session
  {
    private string id;

    private SessionState state;
    private SessionState State
    {
        get { return state; }
        set { state = value; }
    } 
    public Session(string id)
    {
      this.id = id;
      State = SessionState.GREETING;
    }

    /*
      This function reads the state and progresses to the relevant operation
      of our FSM
    */
    public string Input(string from, string input)
    {
      return State switch 
        {
          SessionState.GREETING => Greet(),
          _ => "Not Implemented"
        };
    }
    private string Greet()
    {
      this.State = SessionState.ORDERING;
      return TextManager.Greet();
    }
  }
}