using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


namespace healthHack
{
    public class Board : MonoBehaviour
    {
        private System.Random random;
        public Text selectedCitytext;
        private List<Transform> cities;
        private List<Tuple<Transform, Transform>> paths;
        private List<string> cityNames;
        private List<string> cityNamesOriginal;
        public Text totalPop;
        public Text subPop;
        public Text infPop;
        public Text immunePop;
        public Texture2D nodeTexture;
        public Texture2D pathTexture;
        public Vector2 centrePos;
        public double radius;
        public int numberOfCities;
        public string nameForStats;
        public Transform currentCityTrans;
        public Text vaxText;
        public Text medText;
        public Text mone;
        public int money = 100000000;
        

        private float ticks;


        private bool start;

        void Start()
        {
            start = false;
            random = new System.Random();
            cityNames = new List<string>(new string[] { "ETHEL", "LAUREL", "TABERNASH", "HARDESTY", "AMAGANSETT", "PURVIS", "MARTHA", "WOODWAY", "WATFORD", "METZGER", "RICKETTS", "STEAMBOAT ROCK", "ARLINGTON HEIGHTS", "DRIPPING SPRINGS", "ZILLAH", "CARBON HILL", "SANDY VALLEY", "DUDLEY", "WATERVILLE", "ALTON NORTH", "MOORESVILLE", "PISTAKEE HIGHLANDS", "SAN LUCAS", "ALBERTVILLE", "GRAND JUNCTION", "DUNNELL", "ROWLETT", "WILSON-CONOCOCHEAGUE", "ANDOVER", "ALGONA", "ORCHARD", "VIOLA", "NAPLES", "BOARDMAN", "MIDWAY-HARDWICK", "BELFRY", "FORT LEE", "BREMEN", "NEW POST", "ELKTON", "DANUBE", "PARK RIDGE", "HOUSTON", "LAKE BUTLER", "OGDEN", "COZAD", "CALEDONIA", "GOLDEN VALLEY", "RAYLAND", "TYRONE", "KINGSTON SPRINGS", "CAROLINA", "HUNTINGTON", "BRIARCLIFFE ACRES", "ESPARTO", "ALTOONA", "EDGEWOOD", "BUTLER", "VINE HILL", "STEINAUER", "NEWPORT", "MARION", "BURBANK", "WEST GLENDIVE", "LAKE ANDES", "DAISY", "CHESTERHILL", "BRUNSWICK", "EAST HAMPTON", "UNION", "WHITEWATER", "GARNETT", "HICKSVILLE", "WYOMING", "COMSTOCK", "PORTLAND", "TROY", "ST. FLORIAN", "BASSFIELD", "LAKE ERIE BEACH", "THE VILLAGE OF INDIAN HILL", "NORTH SIOUX CITY", "OVID", "SPRAGUE", "GOLD BAR", "ROCK SPRINGS", "SEDALIA", "KINGSTOWN", "PECAN HILL", "ESTHERVILLE", "NEW HOME", "CAMBRIAN PARK", "COOKEVILLE", "BARNESTON", "REDWATER", "RIO RICO SOUTHWEST", "MUDDY", "SUCCASUNNA-KENVIL", "HALLSVILLE", "NORTH NEWTON", "SOLIS" });
            cityNamesOriginal = new List<string>(cityNames);
            cities = new List<Transform>();
            paths = new List<Tuple<Transform, Transform>>();
            numberOfCities = Difficulty.GetNumOfCities();
            CreateCities(numberOfCities);
            CreateCompleteGraph();
            ReduceGraph();
            selectedCitytext.text = "Selected City";
            ticks = 0;

        }

        public void Update()
        {
            if (start == false)
            {
                int startingInfection = random.Next(cities.Count - 1);
                (cities[startingInfection].gameObject.GetComponent("City") as City).getModel().ExternalInfect(1);
                start = true;
            }

            if (ticks > 1)
            {
                spreadDisease();
                UpdateCities();
                ticks = 0;
            }
            ticks += Time.deltaTime;
        }

        private void spreadDisease()
        {
            foreach (Tuple<Transform, Transform> path in paths)
            {
                Debug.Log((path.Item2.gameObject.GetComponent("City") as City).name);
                if ((path.Item1.gameObject.GetComponent("City") as City).spreads(path.Item2.gameObject.GetComponent("City") as City))
                {
                    (path.Item2.gameObject.GetComponent("City") as City).getModel().ExternalInfect(1);
                } else if ((path.Item2.gameObject.GetComponent("City") as City).spreads(
                    path.Item1.gameObject.GetComponent("City") as City))
                {
                    (path.Item1.gameObject.GetComponent("City") as City).getModel().ExternalInfect(1);

                }
            }
        }

        private void UpdateCities()
        {
            foreach (Transform c in cities)
            {
                City city = (c.gameObject.GetComponent("City") as City);
                city.getModel().Update();
                (c.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).color =
                    new Color(1.0f, 1.0f - (float)(Math.Log10(city.getModel().GetInfected()) / Math.Log10(city.getModel().GetTotalPopulation())), 1.0f - (float)(Math.Log10(city.getModel().GetInfected()) / Math.Log10(city.getModel().GetTotalPopulation())));
                Debug.Log(city.getModel().GetInfected() / city.getModel().GetTotalPopulation());

            }
        }

        public void CreateCities(int numberOfCities)
        {
            double x, y, change = 2 * Math.PI / numberOfCities;
            for (int i = 0; i < numberOfCities; i++)
            {
                var angle = i * change;

                y = radius * Math.Sin(angle) + centrePos.y;
                x = radius * Math.Cos(angle) + centrePos.x;

                var cityIndex = random.Next(cityNames.Count);
                var cityName = cityNames[cityIndex];

                var spriteTransform = InstantiateSpriteObject<City>(nodeTexture, 500f);
                spriteTransform.position = new Vector2((float)x, (float)y);
                spriteTransform.name = cityName;
                spriteTransform.gameObject.AddComponent<BoxCollider>();
                

                cityNames.RemoveAt(cityIndex);
                cities.Add(spriteTransform);
                currentCityTrans = spriteTransform;
            }           
        }

        public void CreateCompleteGraph()
        {
            for (int k = 0; k < cities.Count; k++)
            {
                for (int j = k + 1; j < cities.Count; j++)
                {
                    paths.Add(new Tuple<Transform, Transform>(cities[k], cities[j]));
                }
            }
        }

        public void ReduceGraph()
        {
            var numberToRemove = (int)Math.Ceiling(paths.Count * 0.2);
            int i = 0;
            while (i++ < numberToRemove)
            {
                var index = random.Next(1, paths.Count);
                paths.RemoveAt(index);
            }

            float j = 0;

            {
                foreach (Tuple<Transform, Transform> path in paths)

                   

                {
                    Debug.Log(path);
                    var v1 = path.Item1.position;
                    var v2 = path.Item2.position;
                    var v1tov2 = Math.Sqrt(Math.Pow(v1.x - v2.x, 2) + Math.Pow(v1.y - v2.y, 2));
                    float angle;

                    if (v1.x == v2.x)
                    {
                        angle = 90;
                    } else if (v1.y == v2.y)
                    {
                        angle = 0;
                    } else
                    {
                        angle = (float) (Math.Atan((v1.y - v2.y) / (v1.x - v2.x))*180/Math.PI);                                             
                    }

                    var Transform = InstantiateSpriteObject<Path>(pathTexture, 680f);                                                         
                    Transform.position = new Vector2((float) (v1.x + v2.x) / 2, (float) (v1.y + v2.y) / 2);                                    
                    Debug.Log(angle);                    
                    Transform.Rotate(0, 0, angle);                      
                    float magnitude = Transform.position.magnitude;                                
                    Vector3 newScale = new Vector3 ((float)v1tov2, 1, 0);
                    Transform.localScale = newScale;
                    (Transform.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).color = new Color(j, j + 0.05f, j);
                    Transform.gameObject.AddComponent<BoxCollider>();
                    Transform.name = path.Item1.name + " to " + path.Item2.name;
                    
                    j = j + 0.01f;
                }
            }

        }


        private Transform InstantiateSpriteObject<T>(Texture2D texture, float pixelsPerUnit) where T : MonoBehaviour
        {
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
            var spriteTransform = new GameObject().transform;
            var spriteRenderer = spriteTransform.gameObject.AddComponent<SpriteRenderer>();
            var spriteCity = spriteTransform.gameObject.AddComponent<T>();
            
            spriteRenderer.sprite = sprite;            
            return spriteTransform;
        }

       public void updateStats(String name)
        {
            nameForStats = name;
            City cit;
            if (cityNamesOriginal.Contains(nameForStats))
            {
                selectedCitytext.text = "Selected City: " + nameForStats;
                

                foreach (var trans in cities)
                {
                    if (trans.gameObject.name == nameForStats)
                    {
                        currentCityTrans = trans;
                        cit = trans.gameObject.GetComponent<City>();
                        totalPop.text = "Total Population: " + Math.Ceiling(cit.getModel().GetTotalPopulation());
                        infPop.text = "Infected Population: " + Math.Ceiling(cit.getModel().GetInfected());
                        subPop.text = "Susceptible Population: " + Math.Ceiling(cit.getModel().GetSusceptible());
                        immunePop.text = "Immune Population: " + Math.Ceiling(cit.getModel().GetRecovered());
                       // gameObject.SendMessage("setCity", trans);
                       medText.text = "0";
                       vaxText.text = "0";


                    }
                    
                }
            }

        }

       public void refresh()
       {

          money += 1000;
           mone.text = "Money: $ " + money;
           City cit;
           
           float  totalCost = 0.0f;
           foreach (var trans in cities)
           {
               City c = trans.gameObject.GetComponent<City>();
               totalCost += c.getModel().GetLastCost();
               Debug.Log("totalCost: " + totalCost);
           }
           money  = money -  (int) Math.Ceiling(totalCost);

           


           if (cityNamesOriginal.Contains(nameForStats))
           {
               selectedCitytext.text = "Selected City: " + nameForStats;
                

               foreach (var trans in cities)
               {
                   if (trans.gameObject.name == nameForStats)
                   {
                       cit = trans.gameObject.GetComponent<City>();
                       totalPop.text = "Total Population: " + Math.Ceiling(cit.getModel().GetTotalPopulation());
                       infPop.text = "Infected Population: " + Math.Ceiling(cit.getModel().GetInfected());
                       subPop.text = "Susceptible Population: " + Math.Ceiling(cit.getModel().GetSusceptible());
                       immunePop.text = "Immune Population: " + Math.Ceiling(cit.getModel().GetRecovered());


                   }

                   
                   

               }
           }
       }

    }
}

