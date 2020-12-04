using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace WindowsFormsBus
{
    public partial class FormBusStation : Form
    {
        /// <summary>
        /// Объект от класса-коллекции парковок
        /// </summary>
        private readonly BusStationCollection stationCollection;
        /// <summary>
        /// Логгер
        /// </summary>
        private readonly Logger logger;
        public FormBusStation()
        {
            InitializeComponent();
            stationCollection = new BusStationCollection(pictureBoxBusStation.Width,
pictureBoxBusStation.Height);
            logger = LogManager.GetCurrentClassLogger();
        }
        /// <summary>
        /// Заполнение listBoxLevels
        /// </summary>
        private void ReloadLevels()
        {
            int index = listBoxBusStation.SelectedIndex;
            listBoxBusStation.Items.Clear();
            for (int i = 0; i < stationCollection.Keys.Count; i++)
            {
                listBoxBusStation.Items.Add(stationCollection.Keys[i]);
            }
            if (listBoxBusStation.Items.Count > 0 && (index == -1 || index >=
           listBoxBusStation.Items.Count))
            {
                listBoxBusStation.SelectedIndex = 0;
            }
            else if (listBoxBusStation.Items.Count > 0 && index > -1 && index <
           listBoxBusStation.Items.Count)
            {
                listBoxBusStation.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        private void Draw()
        {
            if (listBoxBusStation.SelectedIndex > -1)
            {//если выбран один из пуктов в listBox (при старте программы ни один пунктне будет выбран и может возникнуть ошибка, если мы попытаемся обратиться к элементу listBox)
                Bitmap bmp = new Bitmap(pictureBoxBusStation.Width,
                pictureBoxBusStation.Height);
                Graphics gr = Graphics.FromImage(bmp);
                stationCollection[listBoxBusStation.SelectedItem.ToString()].Draw(gr);
                pictureBoxBusStation.Image = bmp;
            }
        }
        /// <summary>
        /// /// Обработка нажатия кнопки "Добавить парковку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddBusStation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNewLevelName.Text))
            {
                MessageBox.Show("Введите название парковки", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            logger.Info($"Добавили парковку {textBoxNewLevelName.Text}");
            stationCollection.AddBusStation(textBoxNewLevelName.Text);
            ReloadLevels();
        }
        /// <summary>
        /// Обработка нажатия кнопки "Удалить парковку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelBusStation_Click(object sender, EventArgs e)
        {
            if (listBoxBusStation.SelectedIndex > -1)
            {
                if (MessageBox.Show($"Удалить парковку {listBoxBusStation.SelectedItem.ToString()}?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    {
                        logger.Info($"Удалили парковку{ listBoxBusStation.SelectedItem.ToString()}");
                        stationCollection.DelBusStation(listBoxBusStation.SelectedItem.ToString());
                        ReloadLevels();
                        Draw();
                    }
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Добавить автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetBus_Click(object sender, EventArgs e)
        {
            var formBusConfig = new FormBusConfig();
            formBusConfig.AddEvent(AddBus);
            formBusConfig.Show();
        }
        /// <summary>
        /// Метод добавления машины
        /// </summary>
        
        private void AddBus(Vehicle bus)
        {
            if (bus != null && listBoxBusStation.SelectedIndex > -1)
            {
                try
                {
                    if ((stationCollection[listBoxBusStation.SelectedItem.ToString()]) + bus)
                    {
                        Draw();
                        logger.Info($"Добавлен автобус {bus}");
                    }
                    else
                    {
                        MessageBox.Show("Автобус не удалось поставить");
                        logger.Warn($"Автобус не удалось поставить");
                    }
                    Draw();
                }
                catch (BusStationOverflowException ex)
                {
                    MessageBox.Show(ex.Message, "Переполнение", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                    logger.Warn($"{ex.Message} Переполнение");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Неизвестная ошибка",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Warn($"{ex.Message} Неизвестная ошибка");
                }
            }
        }


        /// <summary>
        /// Обработка нажатия кнопки "Забрать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTakeBus_Click(object sender, EventArgs e)
        {
            if (listBoxBusStation.SelectedIndex > -1 && maskedTextBoxBus.Text != "")
            {
                try
                {
                    var bus = stationCollection[listBoxBusStation.SelectedItem.ToString()] -
                   Convert.ToInt32(maskedTextBoxBus.Text);
                    if (bus != null)
                    {
                        FormBus form = new FormBus();
                        form.SetBus(bus);
                        form.ShowDialog();
                        logger.Info($"Изъят автомобиль {bus} с места{ maskedTextBoxBus.Text} ");
                        Draw();
                    }
                }
                catch (BusStationNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Не найдено", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                    logger.Warn($"{ex.Message} Не найдено");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Неизвестная ошибка",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Warn($"{ex.Message} Неизвестная ошибка");
                }
            }

        }
        /// Метод обработки выбора элемента на listBoxLevels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxBusStation_SelectedIndexChanged(object sender, EventArgs e)
            {
            logger.Info($"Перешли на парковку{ listBoxBusStation.SelectedItem.ToString()}");
                Draw();
            }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogBus.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    stationCollection.SaveData(saveFileDialogBus.FileName);
                    MessageBox.Show("Сохранение прошло успешно", "Результат",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Сохранено в файл " + saveFileDialogBus.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Неизвестная ошибка при сохранении",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Warn($"{ex.Message} Неизвестная ошибка при сохранении");
                }

            }
        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (openFileDialogBus.ShowDialog() == DialogResult.OK)
           {
              try
              {
                stationCollection.LoadData(openFileDialogBus.FileName);
                MessageBox.Show("Загрузили", "Результат", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                logger.Info("Загружено из файла " + openFileDialogBus.FileName);
                ReloadLevels();
                Draw();
              }
              catch (BusStationOccupiedPlaceException ex)
              {
                MessageBox.Show(ex.Message, "Занятое место", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                logger.Warn($"{ex.Message} Занятое место");
              }
              catch (Exception ex)
              {
                MessageBox.Show(ex.Message, "Неизвестная ошибка при сохранении",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Warn($"{ex.Message} Неизвестная ошибка при загрузке");
              }
            }
        }
    }
}

    
