using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WindowsFormsBus
{
    public class Trolleybus : Bus
    {
        public Color DopColor { private set; get; }
        /// Признак наличия штангового токоприемника
        /// </summary>
        public bool RodPantograph { private set; get; }
        /// Признак наличия дверей
        /// </summary>
        public bool Doors { private set; get; }
        public bool Strip{ private set; get; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес автомобиля</param>   
        /// <param name="mainColor">Основной цвет кузова</param>
        /// <param name="dopColor">Дополнительный цвет</param>
        /// <param name="rodPantograph">Признак наличия переднего спойлера</param>
        /// <param name="doors">Признак наличия дверей</param>
        public Trolleybus(int maxSpeed, float weight, Color mainColor, Color dopColor,
bool rodPantograph, bool doors, bool strip) :
 base(maxSpeed, weight, mainColor, 300, 100)
        {
            DopColor = dopColor;
            RodPantograph = rodPantograph;
            Doors = doors;
            Strip = strip;
        }


        /// Отрисовка троллейбуса
        /// </summary>
        /// <param name="g"></param>
        public override void DrawTransport(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Pen penFramework = new Pen(Color.White);
            base.DrawTransport(g);
            if (Strip)
            {
                //Белая полоса
                Brush brWhite = new SolidBrush(Color.White);
                g.FillRectangle(brWhite, _startPosX, _startPosY + 20, 200, 20);
            }
            
            //Отрисовка рога
            if (RodPantograph)
            {

                g.DrawLine(pen, _startPosX + 100, _startPosY, _startPosX + 20, _startPosY - 20);
                g.DrawLine(pen, _startPosX + 20, _startPosY - 20, _startPosX + 10, _startPosY - 20);
                g.DrawLine(pen, _startPosX + 100, _startPosY, _startPosX + 20, _startPosY - 20);
            }
           

            //Отрисовка дверей
            if (Doors)
            {
                Brush dopcolor = new SolidBrush(DopColor);
                g.FillRectangle(dopcolor, _startPosX + 10, _startPosY + 10, 30, 40);
                
                g.FillRectangle(dopcolor, _startPosX + 150, _startPosY + 10, 30, 40);

                //рамки
                
                g.DrawRectangle(penFramework, _startPosX + 10, _startPosY + 10, 30, 40);
               
                g.DrawRectangle(penFramework, _startPosX + 150, _startPosY + 10, 30, 40);

                g.DrawLine(penFramework, _startPosX + 25, _startPosY + 10, _startPosX + 25, _startPosY + 50);//левая
                g.DrawLine(penFramework, _startPosX + 10, _startPosY + 30, _startPosX + 40, _startPosY + 30);

                g.FillRectangle(dopcolor, _startPosX + 80, _startPosY + 10, 30, 40);
                g.DrawRectangle(penFramework, _startPosX + 80, _startPosY + 10, 30, 40);
                g.DrawLine(penFramework, _startPosX + 95, _startPosY + 10, _startPosX + 95, _startPosY + 50);//середина
                g.DrawLine(penFramework, _startPosX + 80, _startPosY + 30, _startPosX + 110, _startPosY + 30);

                g.DrawLine(penFramework, _startPosX + 165, _startPosY + 10, _startPosX + 165, _startPosY + 50);//правая
                g.DrawLine(penFramework, _startPosX + 150, _startPosY + 30, _startPosX + 180, _startPosY + 30);
            }
            
            /// <summary>
            /// Смена дополнительного цвета
            /// </summary>
            /// <param name="color"></param>          

        }
        public void SetDopColor(Color color)
        {
            DopColor = color;
        }
    }
}
