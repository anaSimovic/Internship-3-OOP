using System;

namespace ProjectManagerApp
{
    public static class Utility
    {
        public static T GetValidInput<T>(string prompt, Func<string, (bool IsValid, T Result)> parseFunc)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();
                var (isValid, result) = parseFunc(input);
                if (isValid) return result;
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}
