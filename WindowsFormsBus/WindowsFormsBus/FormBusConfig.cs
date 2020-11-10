﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsBus
{
    
    public partial class FormBusConfig : Form
    {
        
        /// <summary>
        /// Переменная-выбранный транспорт
        /// </summary>
        Vehicle  bus = null;
        private event Action<Vehicle> eventAddBus;
        public FormBusConfig()
        {
            InitializeComponent();
            this.panelGray.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelYellow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelOrange.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelFuchsia.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelGreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelBlack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelBlue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelRed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            buttonClose.Click += (object sender, EventArgs e) => { Close(); };
        }
        private void DrawBus()
        {
            if (bus != null)
            {
                Bitmap bmp = new Bitmap(pictureBoxConfig.Width, pictureBoxConfig.Height);
                Graphics gr = Graphics.FromImage(bmp);
                bus.SetPosition(30, 40, pictureBoxConfig.Width, pictureBoxConfig.Height);
                bus.DrawTransport(gr);
                pictureBoxConfig.Image = bmp;
            }
        }
        /// <summary>
 /// Добавление события
 /// </summary>

 /// <param name="ev"></param>
       public void AddEvent(Action<Vehicle> ev)
        {
            if (eventAddBus == null)
            {
                eventAddBus = new Action<Vehicle>(ev);
            }
            else
            {
                eventAddBus += ev;
            }
        }

        /// <summary>
        /// Передаем информацию при нажатии на Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelBus_MouseDown(object sender, MouseEventArgs e)
        {
            labelBus.DoDragDrop(labelBus.Text, DragDropEffects.Move |
DragDropEffects.Copy);
        }

        private void labelTrolleybus_MouseDown(object sender, MouseEventArgs e)
        {
            labelTrolleybus.DoDragDrop(labelTrolleybus.Text, DragDropEffects.Move |
DragDropEffects.Copy);
        }
        /// <summary>
        /// Проверка получаемой информации (ее типа на соответствие требуемому)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void panelBus_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
       
 /// Действия при приеме перетаскиваемой информации
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>
        private void panelBus_DragEnter(object sender, DragEventArgs e)
        {
            switch (e.Data.GetData(DataFormats.Text).ToString())
            {
                case "Обычный":
                    bus = new Bus((int)numericUpDownSpeed.Value,
(int)numericUpDownWeight.Value, Color.White); ;
                    break;
                case "Троллейбус":
                    bus = new Trolleybus((int)numericUpDownSpeed.Value,
                   (int)numericUpDownWeight.Value, Color.White, Color.Black,
                    checkBoxRod.Checked, checkBoxDoors.Checked, checkBoxStrip.Checked);
                    break;
            }
            DrawBus();
        }

        private void labelMainColor_DragDop(object sender, DragEventArgs e)
        {
            bus.SetMainColor((Color)e.Data.GetData(typeof(Color)));
            DrawBus();
        }

        private void labelMainColor_DragEnter(object sender, DragEventArgs e)
        {
            
            
            if (e.Data.GetDataPresent(typeof(Color)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void labelDopColor_DragDop(object sender, DragEventArgs e)
        {
            if (bus is Trolleybus)
            {
                (bus as Trolleybus).SetDopColor((Color)e.Data.GetData(typeof(Color)));
                DrawBus();
            }
        }
     
        private void panelColor_MouseDown(object sender, MouseEventArgs e)
        {
            Color color = (sender as Panel).BackColor;
            (sender as Panel).DoDragDrop(color, DragDropEffects.Move | DragDropEffects.Copy);

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            eventAddBus?.Invoke(bus);
            Close();
        }
        
    }   
}
