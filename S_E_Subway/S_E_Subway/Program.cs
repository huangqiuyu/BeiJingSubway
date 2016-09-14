using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

//using System.Text;

namespace S_E_Subway
{
    class Program
    {
        public static map maps = new map();
        static void Main(string[] args)
        {
            
            maps.initial();
            while(true)
              maps.printLine(Console.ReadLine());
        }

        
    }
}




class station
{
    public int id;
    public string name;

    private List<int> nearbyId;
    private List<int> belong;

    public station(int i,string n)
    {
        id = i;
        name = n;
        nearbyId = new List<int>();
        belong = new List<int>();
    }

    public void setId(int i)
    {
        id = i;
    }

    public int getId()
    {
        return this.id;
    }

    public string getName()
    {
        return name;
    }

    public List<int> getNearby()
    {
        return nearbyId;
    }

    public void addNearby(int i)
    {
        nearbyId.Add(i);
    }
    public void addBelong(int i)
    {
        belong.Add(i);
    }

}

class map
{
    private List<station> stations;
    private List<line> lines;

    private int idAmount;

    public  map()
    {
        stations = new List<station>();
        lines = new List<line>();
        idAmount = 1;
    }

    public void initial()
    {
        StreamReader sr = new StreamReader("beijing-subway.txt", Encoding.Default);
        string l;
        
        while((l=sr.ReadLine())!=null)
        {
            line temp = new line(l);


            switch (l)
            {
                case "1号线":
                    lines.Add(temp);
                    temp.setId(1);
                    break;
                case "2号线":
                    temp.setId(2);
                    lines.Add(temp);
                    break;
                case "4号线|大兴线":
                    temp.setId(4);
                    lines.Add(temp);
                    break;
                case "5号线":
                    temp.setId(5);
                    lines.Add(temp);
                    break;
                case "6号线":
                    temp.setId(6);
                    lines.Add(temp);
                    break;
                case "7号线":
                    temp.setId(7);
                    lines.Add(temp);
                    break;
                case "8号线":
                    temp.setId(8);
                    lines.Add(temp);
                    break;
                case "9号线":
                    temp.setId(9);
                    lines.Add(temp);
                    break;
                case "10号线":
                    temp.setId(10);
                    lines.Add(temp);
                    break;
                case "13号线":
                    temp.setId(13);
                    lines.Add(temp);
                    break;
                case "14号线东段":
                    temp.setId(1401);
                    lines.Add(temp);
                    break;
                case "14号线西段":
                    temp.setId(1402);
                    lines.Add(temp);
                    break;
                case "15号线":
                    temp.setId(15);
                    lines.Add(temp);
                    break;
                case "八通线":
                    temp.setId(80);
                    lines.Add(temp);
                    break;
                case "昌平线":
                    temp.setId(20);
                    lines.Add(temp);
                    break;
                case "亦庄线":
                    temp.setId(21);
                    lines.Add(temp);
                    break;
                case "房山线":
                    temp.setId(22);
                    lines.Add(temp);
                    break;
                case "机场线":
                    temp.setId(23);
                    lines.Add(temp);
                    break;
               default:
                    Console.WriteLine("Error!");
                    break;
            }
            if((l = sr.ReadLine()) != null)
            {
                string[] words = l.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                   
                    if(isFind(words[i]))
                    {
                        station t = findStation(words[i]);
                        temp.addStation(t);
                        t.addBelong(temp.getId());
                        if (i > 0)
                        {
                            station pre = findStation(words[i - 1]);
                            if (t != null && pre != null)
                            {
                                t.addNearby(pre.getId());
                                pre.addNearby(t.getId());
                            }

                        }
                    }
                    else
                    {
                        station t = new station(idAmount,words[i]);
                        temp.addStation(t);
                        t.addBelong(temp.getId());

                        stations.Add(t);
                        idAmount++;
                        if (i > 0)
                        {
                            station pre = findStation(words[i - 1]);
                            if (t != null && pre != null)
                            {
                                t.addNearby(pre.getId());
                                pre.addNearby(t.getId());
                            }

                        }
                    }

                    
                }
                    
            }


            l = sr.ReadLine();


        }
    }

    private Boolean isFind(string n)
    {
        for(int i=0;i< stations.Count(); i++)
        {
            if (n == stations[i].getName())
                return true;
            
        }

        return false;
    }

    private station findStation(string n)
    {
        for (int i = 0; i < stations.Count(); i++)
        {
            if (n == stations[i].getName())
                return stations[i];
        }

        return null;
    }

    public void printLine(string lineName)
    {
        if(isFindLine(lineName))
        {
            findLine(lineName).print();
        }

        else
        {
            Console.WriteLine("Error!\nThere have not that line!");
        }
    }

    private Boolean isFindLine(string n)
    {
        for (int i = 0; i < lines.Count(); i++)
        {
            if (n == lines[i].getName())
                return true;

        }

        return false;
    }

    private line findLine(string n)
    {
        for (int i = 0; i < lines.Count(); i++)
        {
            if (n == lines[i].getName())
                return lines[i];
        }

        return null;
    }

}

class line
{
    private List<station> way;
    private string name;
    private int id;

    public line(string n)
    {
        name = n;
        id = 0;
        way = new List<station>();
    }

    public void setId(int i)
    {
        id = i;
    }

    public void addStation(station s)
    {
        way.Add(s);
    }

    public int getId()
    {
        return id;
    }

    public string getName()
    {
        return name;
    }

    public void print()
    {
        Console.WriteLine("地铁"+name+"的所有站点为：");
        for(int i=0;i<way.Count(); i++)
        {
            Console.Write(way[i].getName()+ " ");
        }
        Console.WriteLine();
    }
}