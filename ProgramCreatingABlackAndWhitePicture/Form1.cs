namespace ProgramCreatingABlackAndWhitePicture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP; *.JPG;*.GIF; *.PNG)|*.BMP; *.JPG;*.GIF; *.PNG| All files(*.*)|*.* ";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                }
                catch
                {
                    MessageBox.Show("нельзя открыть файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(pictureBox2.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Сохранить картинку как...";
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.CheckPathExists = true;
                saveFileDialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*JPG|Image Files(*.PNG)|*PNG";
                saveFileDialog.ShowHelp = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox2.Image.Save(saveFileDialog.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Изображение не сохранено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } 
            }
        }

        private void grayButton_Click(object sender, EventArgs e)
        {
            if(pictureBox1 != null)
            {
                Bitmap input = new Bitmap(pictureBox1.Image);
                Bitmap output = new Bitmap(input.Width, input.Height);
                for(int j = 0; j < input.Height; j++)
                {
                    for(int i = 0; i < input.Width; i++)
                    {
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        float R = (float)((pixel & 0x00FF0000) >> 16);
                        float G = (float)((pixel & 0x0000FF00) >> 8);
                        float B = (float)(pixel & 0x000000ff);

                        R = G = B = (R + G + B) / 3.0f;

                        UInt32 newPixel = (UInt32)(0xFF0000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B));
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                    pictureBox2.Image = output;
                }
            }
        }
    }
}