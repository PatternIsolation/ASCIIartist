using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASCIIartist.UTIL
{
    public class ImageReader
    {
        public Color[,] imageArray;
        public Char[,] charArray;

        public delegate char Mappeador(Color color);

        public char MapperUno(Color color)
        {
            Byte[] temp = new byte[1];
            temp[0] = color.R ;

            if (BitConverter.ToInt16(temp,0) < 1600) return 'A';
            else return 'B';

        }

        
        public ImageReader(Bitmap bitmap )
        {
            imageArray = new Color[bitmap.Width , bitmap.Height];

            for (int i = 0 ; i< bitmap.Width ; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    imageArray[i, j] = bitmap.GetPixel(i, j);
                }
            }
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charArray.GetLength(0); i++)
            {
                for (int j = 0; j < charArray.GetLength(0); j++)
                {
                    sb.Append(charArray[i, j]);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }


        public void Mappear(Mappeador mappeador)
        {
            for (int i = 0 ; i< imageArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageArray.GetLength(0); j++)
                {
                    charArray[i, j] = mappeador(imageArray[i, j]);
                }
            }
        }
    }
}
