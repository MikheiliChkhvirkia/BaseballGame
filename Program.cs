using BasketballGame.RuleValidationModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketballGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            var inputArray = new string[] { "5", "2", "C", "D", "+" };
            var result = CalPoints(inputArray);
            Console.WriteLine($"Answer = {result}");
        }

        public static int CalPoints(string[] ops)
        {
            var intList = new List<int>();

            short number = 0;

            foreach (var opsItem in ops)
            {
                if(Int16.TryParse(opsItem, out number))
                {
                    intList.Add(number);
                }
                else
                {
                    if(opsItem == "C")
                    {
                        var correctRule = RuleChecker(intList, (int)RulesEnum.Rules.C);
                        if (correctRule.IsCorrect)
                        {
                            intList.RemoveAt(intList.Count - 1);
                        }
                        else
                        {
                            Console.WriteLine(correctRule.Respond);
                            return -100;
                        }
                    }
                    else if(opsItem == "D")
                    {
                        var correctRule = RuleChecker(intList, (int)RulesEnum.Rules.D);
                        if (correctRule.IsCorrect)
                        {
                            int lastNum = intList[intList.Count - 1];

                            intList.Add(lastNum*2);
                        }
                        else
                        {
                            Console.WriteLine(correctRule.Respond);
                            return -100;
                        }
                    }
                    else
                    {
                        var correctRule = RuleChecker(intList, (int)RulesEnum.Rules.Plus);
                        if (correctRule.IsCorrect)
                        {
                            int count = intList[intList.Count - 1] + intList[intList.Count - 2];

                            intList.Add(count);
                        }
                        else
                        {
                            Console.WriteLine(correctRule.Respond);
                            return -100;
                        }
                    }

                }
            }

            return intList.Sum();
        }

        public static RuleValidation RuleChecker(List<int> checkList, int ruleCheck)
        {
            var result = new RuleValidation();
            if(ruleCheck == (int)RulesEnum.Rules.C || ruleCheck == (int)RulesEnum.Rules.D)
            {
                if(checkList.Count >= 1)
                {
                    result.IsCorrect = true;

                    return result;
                }
                result.Respond = $"There has to be at least 1 number before {Enum.GetName(typeof(RulesEnum.Rules), ruleCheck)}";
            }
            if (ruleCheck == (int)RulesEnum.Rules.Plus)
            {
                if (checkList.Count >= 2)
                {
                    result.IsCorrect = true;

                    return result;
                }
                result.Respond = $"There has to be at least 2 numbers before + - ({Enum.GetName(typeof(RulesEnum.Rules), ruleCheck)})";
            }

            return result;
        }

    }
    
}
