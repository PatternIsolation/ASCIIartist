using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace ASCIIartist.UTIL
{
    public class ImageReader
    {
        public Color[,] imageArray;
        public Color[,] imageArrayOriginal;
        public Char[,] charArray;

        public delegate char Mappeador(int dato);
        public delegate int AlgoBW(Color color);
        public delegate Color[,] ResolutionEncoder(Color[,] original);

        #region Algoritmos Black & White
        public int AlgoBWMaxMin(Color color)
        {
            int[] temp = new int[3];
            temp[0] = color.R;
            temp[1] = color.B;
            temp[2] = color.G;

            var coso = from x in temp
                       select x;
            var cosa = (coso.Max(x => (int)x) + coso.Min(x => (int)x)) / 2;

            return cosa % 255;
        }

        public int AlgoBWWeighted(Color color)
        {
            double[] temp = new double[3];
            temp[0] = color.R * 0.21;
            temp[1] = color.B * 0.07;
            temp[2] = color.G * 0.71;

            var coso = from x in temp
                       select x;
            var cosa = coso.Sum();

            return (int)(cosa % 255);
        }

        public int AlgoBWPromedio(Color color)
        {
            int[] temp = new int[3];
            temp[0] = color.R;
            temp[1] = color.B;
            temp[2] = color.G;

            var coso = from x in temp
                       select x;
            var cosa = coso.Average() % 255;

            return (int)cosa;
        }
        #endregion
        
        public char MapperUno(int dato)
        {            
            if (dato < 10) return '.';
            else if (dato < 60) return ':';
            else if (dato < 120) return 'o';
            else if (dato < 180) return 'O';
            else if (dato < 240) return '8';
            else return '@';
        }

        public Color[,] ResEncoderIgualito(Color [,] original)
        {
            Color[,] temp = new Color[original.GetLength(0), original.GetLength(1)];

            for (int i = 0 ; i< temp.GetLength(0) ; i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = original[j, i];                    
                }
            }

            return temp;
        }

        public Color[,] ResEncoderAcostadito(Color[,] original)
        {
            Color[,] temp = new Color[original.GetLength(1), original.GetLength(0)];

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = original[j, i];
                }
            }

            return temp;
        }

        public Color[,] ResEncoderEnCuatros(Color[,] original)
        {
            Color[,] temp = new Color[original.GetLength(0)/4, original.GetLength(1)/4];
            int factor = 2;
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    int tempR = (original[i * factor, j * factor].R + original[i * factor + 1, j * factor].R + original[i, j * factor + 1].R + original[i * factor + 1, j * factor + 1].R) / 4;
                    int tempG = (original[i * factor, j * factor].G + original[i * factor + 1, j * factor].G + original[i * factor, j * factor + 1].G + original[i * factor + 1, j * factor + 1].G) / 4;
                    int tempB = (original[i * factor, j * factor].B + original[i * factor + 1, j * factor].B + original[i * factor, j * factor + 1].B + original[i * factor + 1, j * factor + 1].B) / 4;
                    int tempA = (original[i * factor, j * factor].A + original[i * factor + 1, j * factor].A + original[i * factor, j * factor + 1].A + original[i * factor + 1, j * factor + 1].A) / 4;

                    temp[i, j] = Color.FromArgb(tempA, tempR, tempG, tempB);
                }
            }

            return temp;
        }

                        
        public ImageReader(Bitmap bitmap )
        {
            imageArrayOriginal = new Color[bitmap.Width , bitmap.Height];
            
            for (int i = 0 ; i< bitmap.Width ; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    imageArrayOriginal[i, j] = bitmap.GetPixel(i, j);
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


        public void Mappear(Mappeador mappeador, AlgoBW algoBW, ResolutionEncoder encoder)
        {
            EncodeResolution(encoder);
            charArray = new char[imageArray.GetLength(0), imageArray.GetLength(1)];

            for (int i = 0 ; i< imageArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageArray.GetLength(1); j++)
                {
                    charArray[i, j] = mappeador(algoBW(imageArray[i, j]));
                }
            }
        }

        //public static Bitmap ReadImageFromFile(string filename)
        //{
        //    Bitmap bitmap;

        //    using (Stream stream = new FileStream(filename, FileMode.Open ))
        //    {
        //        bitmap = new Bitmap(stream);                
        //    }
        //    return bitmap;
        //}

        public static Bitmap ReadImageFromFile(string filename)
        {
            Bitmap bitmap;

            bitmap = new Bitmap(filename);
            
            return bitmap;
        }

        void EncodeResolution(ResolutionEncoder encoder)
        {
            imageArray = encoder(imageArrayOriginal );
        }
        


    }
}
