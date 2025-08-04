using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.Jan.LeetCode;

internal class FirstExercises
{
    public static int[] TwoSum(int[] nums, int target)
    {
        int[] result = new int[2];
        int index = 0;
        List<int> usedIndexes = new();

        while (index < nums.Length)
        {
            foreach (int num in nums)
            {
                if (num + nums[index] == target && index != Array.IndexOf(nums, num))
                {
                    result[0] = Array.IndexOf(nums, num);
                    result[1] = index;
                    break;
                }
            }
            index++;
        }

        Console.WriteLine($"{result[0]} {result[1]}");
        return result;
    }

    public static int[] BetterTwoSum(int[] nums, int target)
    {
        Dictionary<int, int> dict = [];

        for (int i = 0; i < nums.Length; i++)
        {
            int num = nums[i];
            int diff = target - num;
            if (dict.ContainsKey(diff))
            {
                return [dict[diff], i];
            }

            dict[num] = i;
        }

        return [-1, -1];
    }

    public int Reverse(int x)
    {
        long absValue = Math.Abs((long)x);
        string xString = absValue.ToString();
        string revX = string.Empty;

        for (int i = xString.Length - 1; i >= 0; i--)
        {
            revX += xString[i];
        }

        if (!int.TryParse(revX, out int result))
            return 0;

        return x < 0 ? -result : result;
    }

    public int BetterReverse(int x)
    {
        int left = x;
        int rev = 0;

        while (Convert.ToBoolean(left))
        {
            int right = left % 10;
            left = left / 10;

            if (rev > int.MaxValue / 10 || (rev == int.MaxValue / 10 && right > 7))
                return 0;
            if (rev < int.MinValue / 10 || (rev == int.MinValue / 10 && right < -8))
                return 0;

            rev = rev * 10 + right;
        }

        return rev;
    }

    internal class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        ListNode combinedList = new ListNode();
        ListNode tail = combinedList;

        while (list1 != null && list2 != null)
        {
            if (list1.val < list2.val)
            {
                tail.next = list1;
                list1 = list1.next;
            }
            else
            {
                tail.next = list2;
                list2 = list2.next;
            }

            tail = tail.next;
        }

        tail.next = list1 ?? list2;

        return combinedList.next;
    }
}