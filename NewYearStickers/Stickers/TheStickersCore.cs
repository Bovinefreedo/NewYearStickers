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

        public int[,] parties { get; set; }
        public MenuElement[][] menu { get; set; }
        public DataExtractor dataExtractor = new();
        public StickerGenerator stickerGenerator = new();
        public SvgGridPdf svgGridPdf = new();

        public TheStickersCore()
        {
            parties = dataExtractor.intsInRange(2, 2, 101, 7, "Hold");
            menu = dataExtractor.getMenuElements();
        }

        public void chooseOptions() {
            Console.WriteLine("Vælg en af følgende");
            Console.WriteLine("1: print alle sedler");
            Console.WriteLine("2: print sedler til en ret");
            Console.WriteLine("3: print sedler til et hold");
            int response = -1;
            bool isInt = int.TryParse(Console.ReadLine(), out response);
            if (isInt) { 
                chooseOptions();
            }
            switch (response) {
                case 1:
                    printAllStickers();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    chooseOptions();
                    return;
            }
        }

        public void printAllStickers() {
            for (int i = 0; i < 5; i++) {
                foreach (MenuElement element in menu[i]) {
                    for (int j = 0; j < parties.GetLength(1); j++) { 
                    
                    }
                }
            }
        }

    }
}
