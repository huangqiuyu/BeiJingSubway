#include<iostream>
#include<fstream>
#include<string>

using namespace std;
using std::string;

class station
{
public:
	int id;
	string name;
	int nearby_id[10];//nearby station's id

private:
	int nearby_length;//how many stations nearby.




public:
	station(int i,string n)
	{
		id = i;
		name = n;
		nearby_length = 0;
		for (int j = 0; j < 10; j++)
			nearby_id[j] = 0;
	}

	//add the nearby station of this station.
	void add_nearby(int i)
	{
		nearby_id[nearby_length] = i;
		nearby_length++;
	}

	int get_id()
	{
		return id;
	}

	string get_name()
	{
		return name;
	}

	int[] get_nearby()
	{
		return nearby_id;
	}
};


station stats[300];
