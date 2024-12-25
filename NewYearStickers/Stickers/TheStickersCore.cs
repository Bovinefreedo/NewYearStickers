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
            parties = dataExtractor.intsInRange(2, 2, 102, 7, "Hold");
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

        public void printElement(int dish, int elementNumber) { 
            List<string> stickerSVG = new List<string>();
            string dishName = menu[dish][elementNumber].name;
            for (int i = 1; i < 100; i++) {
                if (parties[i, dish] == 0)
                    continue;
                string amount = (menu[dish][elementNumber].amount * parties[i, dish]).ToString();
                string people = parties[i,dish].ToString();
                string Svg = stickerGenerator.makeSVG(i.ToString(), people, dishName, amount);
                stickerSVG.Add(Svg);
            }
            dishName = dishName.Replace(" ", "_");
            string outPath = $@"C:\Users\hotso\Documents\Stickers\{dishName}.pdf";
            svgGridPdf.AddSvgGridToPdf(stickerSVG, outPath);
        }

        public void printAllStickers() {
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < menu[i].Length; j++) {
                    printElement(i, j);
                }
            }
        }

    }
}
