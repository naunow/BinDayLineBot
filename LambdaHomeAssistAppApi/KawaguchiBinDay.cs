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
                todaysBin.Add(BinType.ToxicGarbage);
            }

            // 毎週水 プラ
            if ((int)dayOfWeek == 3)
            {
                todaysBin.Add(BinType.PlasticContainers);
            }

            // 第1,3水 ペットボトル・布類
            if ((week == 1 || week == 3) && (int)dayOfWeek == 3)
            {
                todaysBin.Add(BinType.PETBottles);
                todaysBin.Add(BinType.Textiles);
            }

            // 第1,3木 鉄・紙
            if ((week == 1 || week == 3) && (int)dayOfWeek == 4)
            {
                todaysBin.Add(BinType.Metals);
                todaysBin.Add(BinType.Paper);
            }

            // 第2,4水 缶・ビン
            if ((week == 2 || week == 4) && (int)dayOfWeek == 3)
            {
                todaysBin.Add(BinType.Can);
                todaysBin.Add(BinType.Bottles);
            }

            return todaysBin;
        }

    }
}
