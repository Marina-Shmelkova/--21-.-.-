using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsBus
{
    public class BusStation<T> where T : class, ITransport
    {
        /// <summary>
        /// Список объектов, которые храним
        /// </summary>
        private readonly List<T> _places;
        /// <summary>
        /// Максимальное количество мест на парковке
        /// </summary>
        private readonly int _maxCount;

        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private readonly int pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private readonly int pictureHeight;
        /// <summary>
        /// Размер парковочного места (ширина)
        /// </summary>
        private readonly int _placeSizeWidth = 260;
        /// <summary>
        /// Размер парковочного места (высота)
        /// </summary>
        private readonly int _placeSizeHeight = 110;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="picWidth">Рамзер парковки - ширина</param>
        /// <param name="picHeight">Рамзер парковки - высота</param>
        public BusStation(int picWidth, int picHeight)
        {
            int width = picWidth / _placeSizeWidth;
            int height = picHeight / _placeSizeHeight;
            _places = new List<T>();
            pictureWidth = picWidth;
            pictureHeight = picHeight;
            _maxCount = width * height;
        }
        
        /// Перегрузка оператора сложения
        /// Логика действия: на парковку добавляется автобус
        /// </summary>
        /// <param name="p">Автовокзал</param>
        /// <param name="car">Добавляемый автобус</param>
        /// <returns></returns>
        public static bool operator +(BusStation<T> b, T car)
        {
            if (b._places.Count >= b._maxCount)
            {
                return false;
            }
            b._places.Add(car);
            return true;
        }

        /// <summary>
        /// Перегрузка оператора вычитания
        /// Логика действия: с парковки забираем автобус
        /// </summary>
        /// <param name="p">Автовокзал</param>
        /// <param name="index">Индекс места, с которого пытаемся извлечь объект
 public static T operator -(BusStation<T> b, int index)
        {
            if (index < -1 || index > b._places.Count)
            {
                return null;
            }
            T bus = b._places[index];
            b._places.RemoveAt(index);
            return bus;
        }

        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Count; ++i)
            {
                _places[i].SetPosition(5 + i / (pictureHeight / _placeSizeHeight) * _placeSizeWidth + 5, i % (pictureHeight / _placeSizeHeight) *
               _placeSizeHeight + 27, pictureWidth, pictureHeight);
                _places[i].DrawTransport(g);
            }
        }
        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            for (int i = 0; i < pictureWidth / _placeSizeWidth; i++)
            {
                for (int j = 0; j < pictureHeight / _placeSizeHeight + 1; ++j)
                {//линия рамзетки места
                    g.DrawLine(pen, i * _placeSizeWidth + 3, j * _placeSizeHeight + 3, i * _placeSizeWidth + _placeSizeWidth / 2+90, j * _placeSizeHeight + 3);
                }
                g.DrawLine(pen, i * _placeSizeWidth + 3, 3, i * _placeSizeWidth + 3, (pictureHeight / _placeSizeHeight) * _placeSizeHeight + 3);
            }
        }


    }
}
