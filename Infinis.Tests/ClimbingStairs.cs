namespace NeetCode.Beginner.DynamicProgramming;

public class ClimbingStairs
{
    public int ClimbStairs(int n)
    {
        var memo = new Dictionary<int, int>();
        return ResursiveClimb(0, n, memo);
    }

    private int ResursiveClimb(int n, int target, Dictionary<int, int> memo)
    {
        if (memo.ContainsKey(n))
        {
            return memo[n];
        }
        if (n == target)
        {
            return 1;
        }
        if (n > target)
        {
            return 0;
        }
        var sum = 0;
        sum += ResursiveClimb(n + 1, target, memo);
        sum += ResursiveClimb(n + 2, target, memo);
        memo[n] = sum;
        return sum;
    }
    
    public int ClimbStairs2(int n)
    {
        var memo = (1, 1);
        while (n > 0)
        {
            memo = (memo.Item1 + memo.Item2, memo.Item1);
            n--;
        }
        return memo.Item1;
    }
    
    public int Rob(int[] nums)
    {
        var memo = new Dictionary<int, int>();
        return RobRecurse(nums, 0, memo);
    }
    
    public int RobRecurse(int[] nums, int index, Dictionary<int, int> memo){
        if (index < 0 || index >= nums.Length)
        {
            return 0;
        }

        if (memo.ContainsKey(index))
        {
            return memo[index];
        }
        // Rob the current house
        var sum = nums[index] + RobRecurse(nums, index + 2, memo);
        // Skip the current house
        var sumAlt = RobRecurse(nums, index + 1, memo);
        // Return the max of the two
        var max = Math.Max(sum, sumAlt);
        memo[index] = max;
        return max;
    }
    
    public int Rob2(int[] nums)
    {
        var (rob1, rob2) = (0, 0);
        foreach (var num in nums)
        {
            var tmp = Math.Max((rob1 + num), rob2);
            (rob1, rob2) = (tmp, rob1);
        }
        return rob1;
    }
    
}