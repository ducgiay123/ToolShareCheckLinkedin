using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Utils
{
    public class ChromeUtils
    {
        public static int getWidthChrome;
        public static int getHeightChrome;
        public static List<int> listPossitionApp = new List<int>();
        public static int getWidthScreen;
        public static int getHeightScreen;
        public static Point GetPointFromIndexPosition(int indexPos, int maxApp = 12)
        {
            Point location = new Point();
            int widthWindowChrome = (3 * getWidthScreen) / maxApp;
            int totalAppPerLine = maxApp / 3;
            while (indexPos > 11)
            {
                indexPos -= 12;
            }
            if (indexPos <= totalAppPerLine - 1)
            {
                location.Y = 0;
            }
            else if (indexPos < maxApp)
            {
                location.Y = getHeightScreen / 3;
                indexPos -= totalAppPerLine;
            }
            location.X = (indexPos) * (widthWindowChrome);
            return location;
        }
        public static int GetIndexOfPossitionApp()
        {
            int indexPos = 0;
            lock (listPossitionApp)
            {
                for (int i = 0; i < listPossitionApp.Count; i++)
                {
                    if (listPossitionApp[i] == 0)
                    {
                        indexPos = i;
                        listPossitionApp[i] = 1;
                        break;
                    }
                }
            }
            return indexPos;
        }
        public static void FillIndexPossition(int indexPos)
        {
            lock (listPossitionApp)
            {
                listPossitionApp[indexPos] = 0;
            }
        }
    }
}
