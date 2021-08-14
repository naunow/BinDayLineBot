using System;
using System.Collections.Generic;
using System.Text;
using static LambdaHomeAssistAppApi.KawaguchiBinDay;

namespace LambdaHomeAssistAppApi
{
    public class RecycleCalendar
    {
        private List<BinType> _todaysBinType;
        private List<BinType> _tomorrowsBinType;

        public RecycleCalendar(List<BinType> todaysBinType, List<BinType> tomorrowsBinType)
        {
            _todaysBinType = todaysBinType;
            _tomorrowsBinType = tomorrowsBinType;
        }

        public List<RecycleCalendarDetail> TodayBin
        {
            get
            {
                var binList = new List<RecycleCalendarDetail>();
                foreach (var bin in _todaysBinType)
                {
                    var detail = new RecycleCalendarDetail(bin);
                    binList.Add(detail);
                }
                return binList;
            }
        }

        public List<RecycleCalendarDetail> TomorrowsBin
        {
            get
            {
                var binList = new List<RecycleCalendarDetail>();
                foreach (var bin in _tomorrowsBinType)
                {
                    var detail = new RecycleCalendarDetail(bin);
                    binList.Add(detail);
                }
                return binList;
            }
        }
    }
}
