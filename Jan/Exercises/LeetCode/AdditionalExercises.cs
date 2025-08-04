public class AdditionalExercises
{
    public bool IsPalindrome(int x)
    {
        int xLength = x.ToString().Length - 1;

        for (int i = 0; i < xLength; i++)
        {
            char currentDigit = x.ToString().ElementAt(i);
            char currentDigitEnd = x.ToString().ElementAt(xLength - i);

            if (currentDigit != currentDigitEnd)
            {
                return false;
            }
        }

        return true;
    }

    public bool BetterIsPalindrome(int x)
    {
        int reversed = 0;

        if (x < 0 || (x % 10 == 0 && x != 0)) return false;

        while (x > reversed)
        {
            reversed = reversed * 10 + x % 10;
            x /= 10;
        }

        return x == reversed || x == reversed / 10;
    }

    private static readonly Dictionary<char, int> romans;

    static AdditionalExercises()
    {
        romans = new Dictionary<char, int>
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };
    }

    public int RomanToInt(string s)
    {
        int number = 0;

        for (int i = 0; i < s.Length; i++)
        {
            int nextLetterIndex = i + 1;
            char currentLetter = s[i];
            char nextLetter = nextLetterIndex < s.Length ? s[nextLetterIndex] : 'I';

            romans.TryGetValue(currentLetter, out int currentLetterNum);
            romans.TryGetValue(nextLetter, out int nextLetterNum);

            number += currentLetterNum < nextLetterNum ? -currentLetterNum : currentLetterNum;
        }

        return number;
    }

    public string LongestCommonPrefix(string[] strs)
    {
        if (strs == null || strs.Length < 1 || strs[0] == "") return "";
        if (strs.Length == 1) return strs[0];

        int count = 1;
        string commonPrefix = strs[0][..count];

        while (count <= strs[0].Length)
        {
            foreach (string str in strs)
            {
                if (!str.StartsWith(commonPrefix))
                {
                    return commonPrefix[..^1];
                }
            }

            if (count >= strs[0].Length)
                break;

            count++;
            commonPrefix = strs[0][..count];
        }

        return commonPrefix;
    }

    public string BetterLongestCommonPrefix(string[] strs)
    {
        if (strs == null || strs.Length == 0) return "";
        if (strs.Length == 1) return strs[0] ?? "";

        for (int i = 0; i < strs[0].Length; i++)
        {
            char c = strs[0][i];
            foreach (string str in strs)
            {
                if (str == null || i >= str.Length || str[i] != c)
                    return strs[0][..i];
            }
        }
        return strs[0];
    }

    public bool IsValid(string s)
    {
        Dictionary<char, int> bracketCount = new()
        {
            { '(', 0 },
            { ')', 0 },
            { '{', 0 },
            { '}', 0 },
            { '[', 0 },
            { ']', 0 },
        };

        for (int i = 0; i < s.Length; i++)
        {
            char currentBracket = s[i];
            bracketCount[currentBracket]++;
        }
    }
}