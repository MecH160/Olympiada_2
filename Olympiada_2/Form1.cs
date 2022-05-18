using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Olympiada_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        Random rand = new Random();//Создаём рандом для случайного заполнения массива
        
        private void button1_Click(object sender, EventArgs e)
        {
            try//Ловим ошибки неверно введённого формата данных
            {
                if (Int32.Parse(textBox2.Text) > 10 || Int32.Parse(textBox2.Text) < 3)//Проверяем условие об количестве судей (не меньше 3 и не больше 10)
                {
                    MessageBox.Show("Судей может быть только от 3 до 10");//Выводим ошибку условия
                }
                else
                {
                    int judges = Int32.Parse(textBox2.Text);//Кол-во судей
                    int figures = Int32.Parse(textBox1.Text);//Кол-во фигуристов
                    double[,] rating = new double[figures, judges];//Массив оценок

                    int rows = figures + 1;//Строки
                    int columns = judges + 2;//Столбцы
                    dataGridView1.RowCount = rows;//Указываем кол-во строк в таблице
                    dataGridView1.ColumnCount = columns;//Указываем кол-во столбцов в таблице
                    dataGridView1.Rows[0].Cells[0].Value = "Фигуристы/Судьи";//Ячейка в начале таблицы
                    dataGridView1.Rows[0].Cells[columns - 1].Value = "Оценки";//Название последнего столбца
                    for (int i = 1; i < rows; i++)//Цикл заполнения 1 столбца
                    {
                        dataGridView1.Rows[i].Cells[0].Value = $"{i} фигурист";
                    }
                    for (int i = 1; i < columns - 1; i++)//Цикл заполнения 1 строки
                    {
                        dataGridView1.Rows[0].Cells[i].Value = $"{i} судья";
                    }

                    for (int i = 0; i < figures; i++)//Цикл заполнения массива оценок рандомными значениями от 0 до 10.0
                    {
                        for (int j = 0; j < judges; j++)
                        {
                            rating[i, j] = Convert.ToDouble(rand.Next(100) / 10.0 + 0.1);
                        }
                    }

                    for (int i = 1; i < rows; i++)//Заполнение таблицы элементами массива оценок
                    {
                        for (int j = 1; j < columns - 1; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = rating[i - 1, j - 1];
                        }
                    }

                    double[] summa = new double[figures];//Массив оценок
                    for (int i = 0; i < figures; i++)
                    {
                        double s = 0;//Сумма 
                        double max = 0;//Максимум в строке
                        double min = 11;//Минимум в строке
                        for (int j = 0; j < judges; j++)
                        {
                            s += rating[i, j];//Складываем элементы строки в переменную
                            if (max < rating[i, j])//Находим максимум строкм
                            {
                                max = rating[i, j];
                            }
                            if (min > rating[i, j])//Находим минимум строки
                            {
                                min = rating[i, j];
                            }

                        }
                        summa[i] = (s - max - min) / (Convert.ToDouble(judges) - 2);//Нахождение среднего арифмитического(оценки),
                                                                                    //вычитаем из суммы максимум и минимум и
                                                                                    //делим на кол-во судей без максимума и минимума
                    }

                    for (int i = 1; i < rows; i++)//Цикл вывода оценок в таблицу
                    {
                        dataGridView1.Rows[i].Cells[columns - 1].Value = Math.Round(summa[i - 1], 1);
                    }

                    dataGridView1.AutoResizeColumns();//Коррекция столбцов
                    dataGridView1.AutoResizeRows();//Коррекция строк
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//Вывод ошибки неверного формата входных данных
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
