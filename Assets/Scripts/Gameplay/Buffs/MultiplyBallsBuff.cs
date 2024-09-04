public class MultiplyBallsBuff : Buff
{
    private Pool<Ball> _ballPool;
    
    public void Construct(Ball[] currentBalls, Ball ballPrefab)
    {
        _ballPool = new Pool<Ball>(ballPrefab);
    }

    public override void Execute()
    {
        foreach (var VARIABLE in main)
        {
            
        }
    }
}