using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace XML_Sportsmen
{
    class HelperXML
    {
        string errors = "";
        public bool save(string path, Tree tree)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Tree));

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, tree);

                    Console.WriteLine("Объект сериализован");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public object[] read(string pathToXml)
        {
            Stack<Sportsman> stack = new Stack<Sportsman>();

            Tree tree = new Tree();
            errors = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathToXml);

            if (xmlDoc.ChildNodes.Count > 1)
                getTree(stack, xmlDoc.ChildNodes[1].FirstChild);

            stack.Reverse();

            while (stack.Count > 0)
                tree.insert(stack.Pop());
            Console.WriteLine(errors);
            return new object[] { tree, errors };
        }
        private void getTree(Stack<Sportsman> stack, XmlNode parent)
        {
            if (parent == null) return;


            
                string fio, sportType;
                int age, winCount;

                XmlNode node = parent["man"];

                try { 
                    fio = node["fio"].InnerText; 
                    if (fio.Length == 0) 
                        throw new FormatException();
                }
                catch (Exception e) { 
                    fio = "ERROR"; 
                    errors += "Ошибка чтения имени в " + node.InnerText + "\r\n";
                }


                try { 
                    age = int.Parse(node["age"].InnerText);

                } 
                catch (Exception e) { 
                    age = -1; 
                    errors += "Ошибка чтения возраста Спортсмена " + node.InnerText + "\r\n";
                }


                try { 
                    winCount = int.Parse(node["winCount"].InnerText);
                }
                catch (Exception e) { 
                    winCount = -1; 
                    errors += "Ошибка чтения количества побед Спортсмена " + node.InnerText + "\r\n";
                }


                try { 
                    sportType = node["sportType"].InnerText;
                    if (sportType.Length == 0)
                        throw new FormatException();
                }
                catch (Exception e) { 
                    sportType = "ERROR"; 
                    errors += "Ошибка чтения спорта Спортсмена " + node.InnerText + "\r\n";
                }

                Sportsman man = new Sportsman(fio, age, winCount, sportType);
                stack.Push(man);

                getTree(stack, parent["left"]);
                getTree(stack, parent["right"]);

            
        }
    }
}
