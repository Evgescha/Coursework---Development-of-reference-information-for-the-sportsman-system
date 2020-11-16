using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace XML_Sportsmen
{
    public partial class Form1 : Form
    {
        Tree tree = new Tree();

        string pathToXml;
        HelperXML helper = new HelperXML();
        public Form1()
        {
            InitializeComponent();
        }

        public Tree Tree
        {
            get => default;
            set
            {
            }
        }

        internal HelperXML HelperXML
        {
            get => default;
            set
            {
            }
        }

        public Sportsman Sportsman
        {
            get => default;
            set
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pathToXml = DateTime.Now.ToShortDateString().ToString() + ".xml";            
        }              
        
        //сохранить
        private void button4_Click(object sender, EventArgs e)
        {
            pathToXml = DateTime.Now.ToShortDateString().ToString()+" "+ DateTime.Now.ToShortTimeString().ToString().Replace(":","-")+ ".xml";
            if (helper.save(pathToXml, tree))
            {
                MessageBox.Show("Сохранено успешно!");
                readXmlFile();
            }
            else
                MessageBox.Show("Ошибка сохранения!");
        }

        private void readXmlFile()
        {
            //throw new NotImplementedException();
        }

        private bool isFill() {
            if (textBox1.Text.Length < 1 ||
                textBox1.Text.Length < 1 ||
                textBox1.Text.Length < 1 ||
                comboBox1.Text.Length < 1) { 
                MessageBox.Show("Не все поля заполнены"); 
                return false; 
            }
            return true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
        //add
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isFill()) return;
                Sportsman man = new Sportsman(textBox1.Text, int.Parse(textBox2.Text), int.Parse(textBox3.Text), comboBox1.Text);
                tree.insert( man);
                treeToDatagrid();
                MessageBox.Show("Предварительно добавлен. Не забудьте сохранить");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
            }
            catch (Exception ex) {
                MessageBox.Show("Ошибка добавления");
            }
        }
        //выбрать файл
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "xml files (*.xml)|*.xml";
            if (OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox4.Text = "";
                pathToXml = OpenFile.FileName;
                object[]obj= helper.read(pathToXml);
                tree = (Tree)obj[0];
                textBox4.Text = (string)obj[1];
                treeToDatagrid();
            }
        }

        private void treeToDatagrid() {
            dataGridView1.Rows.Clear();

            getSportmanFromTree(tree.root);
        }
        private void getSportmanFromTree(Node parent) {
            if (parent == null) return;
            
            dataGridView1.Rows.Add(new object []{ parent.man.fio, parent.man.age, parent.man.winCount, parent.man.sportType });
            getSportmanFromTree(parent.left);
            getSportmanFromTree(parent.right);

        }
        //delete
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentRow != null) {
                try
                {
                    delete();
                    treeToDatagrid();
                    MessageBox.Show("Удалено! Не забудьте сохранить");
                }
                catch (Exception ex) {
                    MessageBox.Show("Ошибка удаления!");
                }
            }
        }
        private Sportsman delete() {
            string fio, sportType;
            int age, winCount;
            fio = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            age = int.Parse(dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString());
            winCount = int.Parse(dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString());
            sportType = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            Sportsman sportsman = new Sportsman(fio, age, winCount, sportType);

            tree.DeleteNodeFromBinary(ref tree.root, ref tree.root, sportsman.GetHashCode());

            return sportsman;
        }
        //изменить
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.Rows.Count < 1 || dataGridView1.CurrentRow == null) return;
            try
            {
                if (!isFill()) return;
                delete();
                Sportsman man = new Sportsman(textBox1.Text, int.Parse(textBox2.Text), int.Parse(textBox3.Text), comboBox1.Text);
                tree.insert(man);

                treeToDatagrid();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения");
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentRow != null)
            {
                textBox1.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                textBox2.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                textBox3.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                comboBox1.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            }
        }
    }
}
