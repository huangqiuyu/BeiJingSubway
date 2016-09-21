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
      

            if (args[0]=="-b")
            {
                maps.shortest(args[1],args[2]);
            }
            else if(args[0]=="-c")
            {

            }
            else if(args[0]=="-a")
            {

            }
          
            while(true)
            {
                string s = Console.ReadLine();
                if (s == "exit")
                    break;
                maps.printLine(s);
            }
            Console.WriteLine("The program is end. (*^__^*) ");
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

    public List<int> getBelong()
    {
        return belong;
    }

}

class map
{
    private const int MAX = 300;
    private List<station> stations;
    private List<line> lines;

    private int idAmount;

    //used to BFS
    private int[] prevStation;
    private int[] minLen;
    private int[] minBelong;

    private LinkedList<station> shortWay;

    public  map()
    {
        stations = new List<station>();
        lines = new List<line>();
        prevStation = new int[MAX];
        minLen = new int[MAX];
        minBelong = new int[MAX];
        shortWay = new LinkedList<station>();

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

    private station findStation(int n)
    {
        for (int i = 0; i < stations.Count(); i++)
        {
            if (n == stations[i].getId())
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
            Console.WriteLine("Error!\nThere have not that line!\nPlease input the subway line again!");
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

    private line findLine(int n)
    {
        for (int i = 0; i < lines.Count(); i++)
        {
            if (n == lines[i].getId())
                return lines[i];
        }

        return null;
    }

    public void shortest(string a,string b)
    {
        if(isFind(a)&&isFind(b))
        {
            station aa = findStation(a);
            station bb = findStation(b);
            bfs(aa, bb);
            recover(aa, bb);
            printShort(aa, bb);
        }

        else
        {
            Console.WriteLine("Error!\nThere have not that station!\n");
        }
    }

    private void bfs(station a,station b)
    {
        int[] visit = new int[MAX];
        //initial
        for(int i = 0;i<MAX;i++)
        {
            visit[i] = 0;
            prevStation[i] = -1;
            minLen[i] = 0;

        }

        List<station> queue = new List<station>();
        queue.Add(a);

        while(queue.Count!=0)
        {
            Boolean isB = false;
            station current = queue[0];
            queue.RemoveAt(0);
            visit[current.getId()] = 1;
            List<int> nearby = current.getNearby();
            for(int i=0;i<nearby.Count();i++)
            {
                int nearId = nearby[i];
                
                station temp = findStation(nearId);
                if(visit[temp.getId()]==0)
                {
                    prevStation[temp.getId()] = current.getId();
                    visit[temp.getId()] = 1;
                    minLen[temp.getId()] = minLen[current.getId()] + 1;
                    queue.Add(temp);
                }
                if (nearId == b.getId())
                {
                    isB = true;
                    break;
                }
            }

            if (isB)
                break;

        }

    }


    private void recover(station a,station b)
    {
        //recover the way
        shortWay.AddFirst(b);
        station temp = b;
        while(prevStation[temp.getId()]!=a.getId())
        {
            temp = findStation(prevStation[temp.getId()]);
            shortWay.AddFirst(temp);

        }
        shortWay.AddFirst(a);



        //find the transfer
        int i = 0;
        station p = shortWay.ElementAt(0);
        List<int> preBelong = p.getBelong();
        station c;
        List<int> curBelong;
        while(i<(shortWay.Count()-1))
        {
            c = shortWay.ElementAt(i+1);
            curBelong = c.getBelong();


            //find which line of the transfer station belonging 
            for (int j = 0; j < preBelong.Count(); j++)
            {
                int tempPre = preBelong[j];
                for(int r = 0;r<curBelong.Count();r++)
                {
                    int tempCur = curBelong[r];
                    if(tempPre==tempCur)
                    {
                        minBelong[i] = tempPre;
                        break;
                    }
                }
            }
            p = c;
            preBelong = curBelong;

            i++;
        }

    }


    private void printShort(station a,station b)
    {
        Console.WriteLine(minLen[b.getId()]+1);
        Console.WriteLine(a.getName());

        for (int i = 1;i<shortWay.Count();i++)
        {
            if(minBelong[i-1]!=minBelong[i]&&minBelong[i]!=0&&minBelong[i-1]!=0)
            {
                Console.WriteLine(shortWay.ElementAt(i).getName() + "换乘地铁" + findLine(minBelong[i]).getName());
                
            }
            else
            {
                Console.WriteLine(shortWay.ElementAt(i).getName());

            }
        }
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