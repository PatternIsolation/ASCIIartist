using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASCIIartist
{
    public partial class Form1 : Form
    {
        private ASCIIartist.UTIL.ImageReader imageReader;
        public Form1()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ventanita = new OpenFileDialog();
            string filename;
            string filter;

            ventanita.InitialDirectory = @"c:\";            
            filter = "Bitmap files (*.bmp)|*.bmp|";
            filter += "JPG files (*.jpg)|*.jpg|";
            filter += "JPEG files (*.jpeg)|*.jpeg|";
            filter += "GIF files (*.gif)|*.gif|";
            filter += "PNG files (*.png)|*.png";
            ventanita.Filter = filter;
            ventanita.FilterIndex = 1;
            ventanita.RestoreDirectory = true;

            ventanita.CheckFileExists = true;
            ventanita.CheckPathExists = true;
            ventanita.Multiselect = true;

            if (ventanita.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    foreach (string x in ventanita.FileNames)
                    {
                        filename = x;                        
                        this.imageReader = new UTIL.ImageReader(UTIL.ImageReader.ReadImageFromFile(x));
                        imageReader.Mappear(imageReader.MapperUno, imageReader.AlgoBWMaxMin, imageReader.ResEncoderIgualito  );                        
                        this.textBox1.Text = imageReader.ToString();
                    }                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un tunel: " + ex.Message);
                }
            }

           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Font coso = new Font(this.textBox1.Font.FontFamily, (textBox1.Font.Size - 1 > 0 ? textBox1.Font.Size - 1 : textBox1.Font.Size ) , textBox1.Font.Style );
                this.textBox1.Font = coso;
            }
            catch(Exception ex)
            {
                
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Font coso = new Font(this.textBox1.Font.FontFamily, (textBox1.Font.Size + 1 > 0 ? textBox1.Font.Size + 1 : textBox1.Font.Size), textBox1.Font.Style);
                this.textBox1.Font = coso;
            }
            catch (Exception ex)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageReader.Mappear(imageReader.MapperUno, imageReader.AlgoBWMaxMin, imageReader.ResEncoderIgualito);  
            this.textBox1.Text = imageReader.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            imageReader.Mappear(imageReader.MapperUno, imageReader.AlgoBWMaxMin, imageReader.ResEncoderIgualito);  
            this.textBox1.Text = imageReader.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            imageReader.Mappear(imageReader.MapperUno, imageReader.AlgoBWMaxMin, imageReader.ResEncoderIgualito);  
            this.textBox1.Text = imageReader.ToString();
        }

        void LoadFontFamilies()
        {
            List<string> fonts = new List<string>();

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                fonts.Add(font.Name);
            }

            this.comboBox1.DataSource = fonts;            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadFontFamilies();            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Font previo = new Font(textBox1.Font.FontFamily, textBox1.Font.Size, textBox1.Font.Style);
            try
            {
                Font coso = new Font(new FontFamily((string)this.comboBox1.SelectedValue), textBox1.Font.Size, textBox1.Font.Style);
                this.textBox1.Font = coso;
            }
            catch (Exception ex)
            {
                this.textBox1.Font = previo;
            }
            
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            imageReader.Mappear(imageReader.MapperUno, imageReader.AlgoBWMaxMin, imageReader.ResEncoderIgualito);
            this.textBox1.Text = imageReader.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            imageReader.Mappear(imageReader.MapperUno, imageReader.AlgoBWMaxMin, imageReader.ResEncoderAcostadito);
            this.textBox1.Text = imageReader.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            imageReader.Mappear(imageReader.MapperUno, imageReader.AlgoBWMaxMin, imageReader.ResEncoderEnCuatros);
            this.textBox1.Text = imageReader.ToString();
        }
    }
}
