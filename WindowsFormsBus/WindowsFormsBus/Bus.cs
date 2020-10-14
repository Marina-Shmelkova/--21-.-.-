﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WindowsFormsBus
{
    public class Bus : Vehicle
    {
       
        /// Ширина отрисовки троллейбуса
        private readonly int busWidth = 300;
        /// <summary>
        /// Высота отрисовки троллейбуса
        /// </summary>
        private readonly int busHeight = 100;
        /// Загруженность пассажирами 
        /// /// Максимальная скорость
        /// </summary>
        /// Конструктор
        /// </summary>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес автобуса</param>
        /// <param name="mainColor">Основной цвет кузова</param>
        public Bus(int maxSpeed, float weight, Color mainColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
        }
        /// Конструкторс изменением размеров автобуса
        /// </summary>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес автобуса</param>
        /// <param name="mainColor">Основной цвет кузова</param>
        /// <param name="carWidth">Ширина отрисовки автобуса</param>
        /// <param name="carHeight">Высота отрисовки автобуса</param>
        protected Bus(int maxSpeed, float weight, Color mainColor, int busWidth, int
       busHeight)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            this.busWidth = busWidth;
            this.busHeight = busHeight;
        }
        public override void MoveTransport(Direction direction)
        {
            int scope = 40;
            float step = MaxSpeed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - busWidth)
                    {
                        _startPosX += step;
                    }
                    
            break;
                //влево
                case Direction.Left:
                    if (_startPosX - step > scope)
                    {
                        _startPosX -= step;
                    }
                    break;
                //вверх
                case Direction.Up:
                    if (_startPosY - step > scope)
                    {
                        _startPosY -= step;
                    }
                    break;
                //вниз
                case Direction.Down:
                    if (_startPosY + step < _pictureHeight - busHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }
        public override void DrawTransport(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Pen penFramework = new Pen(Color.White);
            //Отрисовка кузова

            Brush br = new SolidBrush(MainColor);
            g.FillRectangle(br, _startPosX, _startPosY, 200, 50);

            //Зеркало
            g.DrawLine(pen, _startPosX + 200, _startPosY + 5, _startPosX + 210, _startPosY + 5);
            g.DrawLine(pen, _startPosX + 210, _startPosY, _startPosX + 210, _startPosY + 15);

            //Колеса         
            Brush brBlack = new SolidBrush(Color.Black);
            g.FillEllipse(brBlack, _startPosX + 50, _startPosY + 48, 22, 22);
            g.FillEllipse(brBlack, _startPosX + 130, _startPosY + 48, 22, 22);

            //Белая полоса
            Brush brWhite = new SolidBrush(Color.White);
            g.FillRectangle(brWhite, _startPosX, _startPosY + 20, 200, 20);
            //окно
            Brush br_yellow = new SolidBrush(Color.Yellow);
            //лобовое
            g.FillRectangle(br_yellow, _startPosX + 190, _startPosY + 10, 10, 30);
            g.DrawRectangle(penFramework, _startPosX + 190, _startPosY + 10, 10, 30);//лобовое

            //дверь
            g.FillRectangle(br_yellow, _startPosX + 80, _startPosY + 10, 30, 40);
            g.DrawRectangle(penFramework, _startPosX + 80, _startPosY + 10, 30, 40);
            g.DrawLine(penFramework, _startPosX + 95, _startPosY + 10, _startPosX + 95, _startPosY + 50);//середина
            g.DrawLine(penFramework, _startPosX + 80, _startPosY + 30, _startPosX + 110, _startPosY + 30);

            g.DrawRectangle(pen, _startPosX, _startPosY, 200, 50);
        }

    }
}
