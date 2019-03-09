using System;
using System.Collections.Generic;
namespace healthHack
{
    public class Board
    {
        private List<City> citys;
        private City[][] graph;
        private int numberOfCities;
        private Tuple<City, City> path;
        private List<Tuple<City, City>> paths;
        private Random random;
        private List<String> listOfCities = new List<string>(new string[] { "ETHEL", "LAUREL", "TABERNASH", "HARDESTY", "AMAGANSETT", "PURVIS", "MARTHA", "WOODWAY", "METZGER", "RICKETTS", "STEAMBOAT ROCK", "ARLINGTON HEIGHTS", "DRIPPING SPRINGS", "ZILLAH", "CARBON HILL", "SANDY VALLEY", "DUDLEY", "WATERVILLE", "ALTON NORTH", "MOORESVILLE", "PISTAKEE HIGHLANDS", "SAN LUCAS", "ALBERTVILLE", "GRAND JUNCTION", "DUNNELL", "ROWLETT", "WILSON-CONOCOCHEAGUE", "ANDOVER", "ALGONA", "ORCHARD", "VIOLA", "NAPLES", "BOARDMAN", "MIDWAY-HARDWICK", "BELFRY", "FORT LEE", "BREMEN", "NEW POST", "ELKTON", "DANUBE", "PARK RIDGE", "HOUSTON", "LAKE BUTLER", "OGDEN", "COZAD", "CALEDONIA", "GOLDEN VALLEY", "RAYLAND", "TYRONE", "KINGSTON SPRINGS", "CAROLINA", "HUNTINGTON", "BRIARCLIFFE ACRES", "ESPARTO", "ALTOONA", "EDGEWOOD", "BUTLER", "VINE HILL", "STEINAUER", "NEWPORT", "MARION", "BURBANK", "WEST GLENDIVE", "LAKE ANDES", "DAISY", "CHESTERHILL", "BRUNSWICK", "EAST HAMPTON", "UNION", "WHITEWATER", "GARNETT", "HICKSVILLE", "WYOMING", "COMSTOCK", "PORTLAND", "TROY", "ST. FLORIAN", "BASSFIELD", "LAKE ERIE BEACH", "THE VILLAGE OF INDIAN HILL", "NORTH SIOUX CITY", "OVID", "SPRAGUE", "GOLD BAR", "ROCK SPRINGS", "SEDALIA", "KINGSTOWN", "PECAN HILL", "ESTHERVILLE", "NEW HOME", "CAMBRIAN PARK", "COOKEVILLE", "BARNESTON", "REDWATER", "RIO RICO SOUTHWEST", "MUDDY", "SUCCASUNNA-KENVIL", "HALLSVILLE", "NORTH NEWTON", "SOLIS" });
        private int availbleNames;

        public Board(int numberOfCity)
        {
            this.random = new Random();
            this.numberOfCities = numberOfCity;
           this.availbleNames = listOfCities.Count;
            citys = new List<City>();
            paths = new List<Tuple<City, City>>();
            while (numberOfCity > 0)
            {
                var cityIndex = random.Next(availbleNames);

                citys.Add(new City(listOfCities[cityIndex]));
                listOfCities.RemoveAt(cityIndex);

                availbleNames--; 
                numberOfCity--;
            }

            for (int k = 0; k < citys.Count; k ++)
            {

                for (int j = k+1;  j < citys.Count; j ++)
                {
                    paths.Add(new Tuple<City, City>(citys[k], citys[j]));
                    //Console.WriteLine( "first: " + paths.Count);
                }
            }

            var numberToRemove = (int)Math.Ceiling(paths.Count * 0.4);
            int i = 0;
            while (i < numberToRemove)
            {

                //Console.WriteLine("second" + paths.Count);
                var index = random.Next(1, paths.Count );

                //Console.WriteLine("Index: " + index);
                paths.RemoveAt(index);
                i++;
            }

            foreach (Tuple<City, City> px in paths)
            {

                Console.WriteLine(px.Item1.toString() + " , " + px.Item2.toString());
            }
            Console.WriteLine(paths.Count);

        }
    }


    }

