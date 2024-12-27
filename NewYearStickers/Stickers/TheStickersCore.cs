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
            parties = dataExtractor.intsInRange();
            menu = dataExtractor.getMenuElements();
        }

        public void chooseOptions()
        {
            Console.WriteLine("Vælg en af følgende");
            Console.WriteLine("1: print alle sedler");
            Console.WriteLine("2: print sedler til en ret");
            Console.WriteLine("3: print sedler til et hold");
            int response = -1;
            bool isInt = int.TryParse(Console.ReadLine(), out response);
            if (isInt)
            {
                chooseOptions();
            }
            switch (response)
            {
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

        public void printElement(int dish, int elementNumber)
        {
            List<string> stickerSVG = new List<string>();
            string dishName = menu[dish][elementNumber].name;
            for (int i = 0; i < 99; i++)
            {
                if (parties[i, dish] == 0)
                    continue;
                string amount = (menu[dish][elementNumber].amount * parties[i, dish]).ToString();
                string people = parties[i, dish].ToString();
                string Svg = stickerGenerator.makeSVG((i + 1).ToString(), people, dishName, amount);
                stickerSVG.Add(Svg);
            }
            dishName = dishName.Replace(" ", "_");
            string outPath = $@"C:\Users\hotso\Documents\Stickers\{dishName}.pdf";
            svgGridPdf.AddSvgGridToPdf(stickerSVG, outPath);
        }

        public void printAllStickers()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < menu[i].Length; j++)
                {
                    printElement(i, j);
                }
            }
        }

        public void printParty(int num)
        {
            int number = num - 1;
            var paramsList = new List<(string hold, string people, string dish, string amount)>();
            List<string> stickerSVG = new List<string>();
            for (int i = 0; i < menu.Length; i++)
            {
                if (parties[number, i] == 0)
                {
                    continue;
                }
                for (int j = 0; j < menu[i].Length; j++)
                {
                    string amount = (parties[number, i] * menu[i][j].amount).ToString();
                    string people = parties[number, i].ToString();
                    string dish = menu[i][j].name;
                    string hold = num.ToString();
                    Console.WriteLine($"{hold} :: {people} x {dish} :: {amount}");
                    paramsList.Add((hold, people, dish, amount));
                }
            }
            foreach (var param in paramsList)
            {
                string svg = stickerGenerator.makeSVG(
                    param.hold,
                    param.people,
                    param.dish,
                    param.amount
                );
                stickerSVG.Add(svg);
            }
            foreach (string sticker in stickerSVG) { 
                Console.WriteLine(sticker);
            }
            var stickerSVGCopy = new List<string>(stickerSVG);

            string outPath = $@"C:\Users\hotso\Documents\Stickers\hold{num}.pdf";
            // Write SVGs to a text file for verification
            File.WriteAllLines($@"C:\Users\hotso\Documents\Stickers\hold{num}_before.txt", stickerSVG);

            svgGridPdf.AddSvgGridToPdf(stickerSVGCopy, outPath);

            // Write the copy after the call to check if anything changed
            File.WriteAllLines($@"C:\Users\hotso\Documents\Stickers\hold{num}_after.txt", stickerSVGCopy);
        }
    }
}
