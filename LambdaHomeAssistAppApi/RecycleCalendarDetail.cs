using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaHomeAssistAppApi
{
    public class RecycleCalendarDetail
    {
        private KawaguchiBinDay.BinType _binType;

        public RecycleCalendarDetail(KawaguchiBinDay.BinType binType)
        {
            _binType = binType;
        }

        public string BinInEnglish
        {
            get
            {
                return _binType switch
                {
                    KawaguchiBinDay.BinType.RegularGarbage => "Regular garbage",
                    KawaguchiBinDay.BinType.ToxicGarbage => "Toxic garbage",
                    KawaguchiBinDay.BinType.PlasticContainers => "Plastic containers",
                    KawaguchiBinDay.BinType.PETBottles => "PET bottles",
                    KawaguchiBinDay.BinType.Textiles => "Textiles",
                    KawaguchiBinDay.BinType.Bottles => "Glass bottles",
                    KawaguchiBinDay.BinType.Can => "Can",
                    KawaguchiBinDay.BinType.Metals => "Metals",
                    KawaguchiBinDay.BinType.Paper => "Paper/Cardboard",
                    _ => throw new Exception("当てはまるゴミ区分がありません。"),
                };
            }
        }

        public string BinInJapanese
        {
            get
            {
                return _binType switch
                {
                    KawaguchiBinDay.BinType.RegularGarbage => "もえるごみ",
                    KawaguchiBinDay.BinType.ToxicGarbage => "有害ごみ",
                    KawaguchiBinDay.BinType.PlasticContainers => "プラ",
                    KawaguchiBinDay.BinType.PETBottles => "ペットボトル",
                    KawaguchiBinDay.BinType.Textiles => "繊維",
                    KawaguchiBinDay.BinType.Bottles => "びん",
                    KawaguchiBinDay.BinType.Can => "かん",
                    KawaguchiBinDay.BinType.Metals => "金属",
                    KawaguchiBinDay.BinType.Paper => "紙/段ボール",
                    _ => throw new Exception("当てはまるゴミ区分がありません。"),
                };
            }
        }
    }
}
