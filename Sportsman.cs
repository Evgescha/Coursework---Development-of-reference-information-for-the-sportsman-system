using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace XML_Sportsmen
{
[Serializable]
   public class Sportsman
    {
        public string fio;
        public int age;
        public int winCount;
        public string sportType;

        public Sportsman(string fio, int age, int winCount, string sportType)
        {
            this.fio = fio;
            this.age = age;
            this.winCount = winCount;
            this.sportType = sportType;
        }

        public Sportsman()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Sportsman sportsman &&
                   fio == sportsman.fio &&
                   age == sportsman.age &&
                   winCount == sportsman.winCount &&
                   sportType == sportsman.sportType;
        }

        public override int GetHashCode()
        {
            var hashCode = 1669081574;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(fio);
            hashCode = hashCode * -1521134295 + age.GetHashCode();
            hashCode = hashCode * -1521134295 + winCount.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(sportType);
            if (hashCode % 2 == 0) hashCode = -hashCode;
            return hashCode;
        }

        public override string ToString()
        {
            return fio+" "+age + " "+winCount + " "+sportType;
        }
    }
}
