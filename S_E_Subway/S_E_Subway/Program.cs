using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using System.Text;

namespace S_E_Subway
{
    class Program
    {
        public static map maps = new map();
        static void Main(string[] args)
        {
            //for (int i = 0; i < args.Length; i++)
            //    Console.WriteLine(args[i]);
            maps.initial();
        }

        
    }
}




class station
{
    public int id;
    public string name;

    private int[] nearbyId;
    private int nearbyLength;

    public station(int i,string n)
    {
        id = i;
        n = name;
        nearbyLength = 0;
        nearbyId = new int[10];
    }

    public int getId()
    {
        return this.id;
    }

    public string getName()
    {
        return name;
    }

    public int[] getNearby()
    {
        return nearbyId;
    }
}

class map
{
    public station[] stations;

    private int amount;

    public  map()
    {
        stations = new station[300];
        amount = 0;
    }

    public void initial()
    {
        StreamReader sr = new StreamReader("beijing-subway.txt", Encoding.Default);
        string line;
        while((line=sr.ReadLine())!=null)
        {
            string[] words = line.Split(' ');
            for(int i=0;i<words.Length;i++)
               Console.WriteLine(words[i]);
        }
    }

    private Boolean isFind(string n)
    {
        for(int i=0;i<amount;i++)
        {
            if (n == stations[i].getName())
                return false;
        }

        return true;
    }

    private int findId(string n)
    {
        for (int i = 0; i < amount; i++)
        {
            if (n == stations[i].getName())
                return i+1;
        }

        return 0;
    }
}
