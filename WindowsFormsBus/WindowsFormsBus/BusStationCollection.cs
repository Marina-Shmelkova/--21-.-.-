using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsBus
{
    public class BusStationCollection
    {
        /// <summary>
        /// Словарь (хранилище) с парковками
        /// </summary>
        readonly Dictionary<string, BusStation<Vehicle>> stationStages;
        /// <summary>
        /// Возвращение списка названий праковок
        /// </summary>
        public List<string> Keys => stationStages.Keys.ToList();
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private readonly int pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private readonly int pictureHeight;
        /// <summary>
        /// Разделитель для записи информации в файл
        /// </summary>
        private readonly char separator = ':';
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pictureWidth"></param>
        /// <param name="pictureHeight"></param>
        public BusStationCollection(int pictureWidth, int pictureHeight)
        {
            stationStages = new Dictionary<string, BusStation<Vehicle>>();
            this.pictureWidth = pictureWidth;
            this.pictureHeight = pictureHeight;
        }
        /// <summary>
        /// Добавление парковки
        /// </summary>
        /// <param name="name">Название парковки</param>
        public void AddBusStation(string name)
        {
            if (stationStages.ContainsKey(name))
            {
                return;
            }
            stationStages.Add(name, new BusStation<Vehicle>(pictureWidth, pictureHeight));
        }
        /// <summary>
        /// Удаление парковки
        /// </summary>
        /// <param name="name">Название парковки</param>
        public void DelBusStation(string name)
        {
            if (stationStages.ContainsKey(name))
            {
                stationStages.Remove(name);
            }
        }
        /// <summary>
        /// Доступ к парковке
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        public BusStation<Vehicle> this[string ind]
        {
            get
            {
                if (stationStages.ContainsKey(ind))
                {
                    return stationStages[ind];
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Метод записи информации в файл
        /// </summary>
        /// <param name="text">Строка, которую следует записать</param>
        /// <param name="stream">Поток для записи</param>
        private void WriteToFile(string text, FileStream stream)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(text);
            stream.Write(info, 0, info.Length);
        }
        /// <summary>
        /// Сохранение информации по автобусам на парковках в файл
        /// </summary>
        /// <param name="filename">Путь и имя файла</param>
        /// <returns></returns>
        public bool SaveData(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                WriteToFile($"BusStationCollection{Environment.NewLine}", fs);
                foreach (var level in stationStages)
                {
                    //Начинаем парковку
                    WriteToFile($"BusStation{separator}{level.Key}{Environment.NewLine}",
                    fs);
                    ITransport bus = null;
                    for (int i = 0; (bus = level.Value.GetNext(i)) != null; i++)
                    {
                        if (bus != null)
                        {
                            //если место не пустое
                            //Записываем тип автобуса
                            if (bus.GetType().Name == "Bus")
                            {
                                WriteToFile($"Bus{separator}", fs);
                            }
                            if (bus.GetType().Name == "Trolleybus")
                            {
                                WriteToFile($"Trolleybus{separator}", fs);
                            }
                            //Записываемые параметры
                            WriteToFile(bus + Environment.NewLine, fs);
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Загрузка нформации по автобусам на парковках из файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool LoadData(string filename)
        {
            if (!File.Exists(filename))
            {
                return false;
            }
            string bufferTextFromFile = "";
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                byte[] b = new byte[fs.Length];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    bufferTextFromFile += temp.GetString(b);
                }
            }
            bufferTextFromFile = bufferTextFromFile.Replace("\r", "");
            var strs = bufferTextFromFile.Split('\n');
            if (strs[0].Contains("BusStationCollection"))
            {
                //очищаем записи
                stationStages.Clear();
            }
            else
            {
                //если нет такой записи, то это не те данные
                return false;
            }
            Vehicle bus = null;
            string key = string.Empty;
            for (int i = 1; i < strs.Length; ++i)
            {
                //идем по считанным записям
                if (strs[i].Contains("BusStation"))
                {//начинаем новую парковку
                    key = strs[i].Split(separator)[1];
                    stationStages.Add(key, new BusStation<Vehicle>(pictureWidth,
                    pictureHeight));
                    continue;
                }
                if (string.IsNullOrEmpty(strs[i]))
                {
                    continue;
                }
                if (strs[i].Split(separator)[0] == "Bus")
                {
                    bus = new Bus(strs[i].Split(separator)[1]);
                }
                else if (strs[i].Split(separator)[0] == "Trolleybus")
                {
                    bus = new Trolleybus(strs[i].Split(separator)[1]);
                }
                var result = stationStages[key] + bus;
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
