using NewYearStickers.Extraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearStickers.Stickers
{
    public class TheStickersCore
    {

        public int[,] parties { get; set; } = new int[1,1];
        public MenuElement[][]? menu = new MenuElement[5][];
        public DataExtractor dataExtractor = new();
        public StickerGenerator stickerGenerator = new();
        public SvgGridPdf svgGridPdf = new();

        public TheStickersCore() {
            parties = dataExtractor.intsInRange(2, 2, 101, 7, "Hold");
            for (int i = 0; i < 5; i++)
            {
                List<MenuElement> elements = new List<MenuElement>();
                int j = 0;
                while (true)
                {
                    List<string> list = dataExtractor.getCellsInRow(2+j, (i*2) + 1, (i*2)+ 2, "Menu");
                    if (list[0] == string.Empty || list[1] == string.Empty) { 
                        break;
                    }
                    MenuElement item = new MenuElement
                    {
                        name = list[0],
                        amount = int.Parse(list[1]),
                    };
                    elements.Add(item);
                    j++;
                }
                menu[i] = elements.ToArray();
            }
        }
    }
}
