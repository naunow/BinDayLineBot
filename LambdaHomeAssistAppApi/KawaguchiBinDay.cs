using System;
using System.Collections.Generic;
using System.Text;
using static LambdaHomeAssistAppApi.Function;

namespace LambdaHomeAssistAppApi
{
    public class KawaguchiBinDay
    {
        public enum BinType
        {
            RegularGarbage,
            ToxicGarbage,
            PlasticContainers,
            PETBottles,
            Textiles,
            Bottles,
            Can,
            Metals,
            Paper,
            Glasses,
            Batteries,
            Potteries,
        }

        public static RecycleCalendar GetBinSchedule(DateTime today)
        {
            List<BinType> todaysBin = GetBinScheduleByDay(today);
            List<BinType> tomorrowsBin = GetBinScheduleByDay(today.AddDays(1));

            return new RecycleCalendar(todaysBin, tomorrowsBin);
        }

        private static List<BinType> GetBinScheduleByDay(DateTime today)
        {
            var dayOfWeek = today.DayOfWeek;
            var day = today.Day;
            int week = day / 7;

            if (day % 7 > 0)
            {
                week++;
            }

            List<BinType> todaysBin = new List<BinType>();

            // 毎週月木 もえるごみ
            if ((int)dayOfWeek == 1 || (int)dayOfWeek == 4)
            {
                todaysBin.Add(BinType.RegularGarbage);
            }

            // 第1,3火 ペットボトル
            if ((week == 1 || week == 3) && dayOfWeek == DayOfWeek.Tuesday)
            {
                todaysBin.Add(BinType.PETBottles);
            }

            // 第1水 鉄・陶器・充電式小型家電
            if (week == 1 && dayOfWeek == DayOfWeek.Wednesday)
            {
                todaysBin.Add(BinType.Metals);
                todaysBin.Add(BinType.Potteries);
                todaysBin.Add(BinType.Batteries);
            }

            // 第2水 ガラス
            if (week == 2 && dayOfWeek == DayOfWeek.Wednesday)
            {
                todaysBin.Add(BinType.Glasses);
            }

            // 第3水 飲料缶・スプレー缶
            if (week == 3 && dayOfWeek == DayOfWeek.Wednesday)
            {
                todaysBin.Add(BinType.Can);
            }

            // 第4水 紙・布
            if (week == 4 && dayOfWeek == DayOfWeek.Wednesday)
            {
                todaysBin.Add(BinType.Paper);
                todaysBin.Add(BinType.Textiles);
            }

            return todaysBin;
        }

    }
}
