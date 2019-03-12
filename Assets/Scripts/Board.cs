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
        private static System.Random random;
        private List<Transform> cityTransforms;
        private List<Tuple<Transform, Transform>> paths;
        private List<string> cityNames;
        private List<string> cityNamesOriginal;        
        private int money;
        private string currentSelectedCity;        
        private bool gameStarted;
        private int income;

        public Text SelectedCityText;        
        public Text MoneyText;
        public Text TotalPopText;
        public Text SusPopText;
        public Text InfPopText;
        public Text ImmunePopText;
        public Texture2D NodeTexture;
        public Texture2D PathTexture;
        public double Radius;
        public Vector2 CentrePos;

        public InputField VaxField;
        public InputField MedField;

        public Transform CurrentCityTransform
        {
            get => cityTransforms.Find(t => t.gameObject.name == currentSelectedCity);                            
        }        

        void Start()
        {
            money = 10000000;
            income = 10000;
            gameStarted = false;
            random = new System.Random();
            cityNames = new List<string>(new string[] { "ETHEL", "LAUREL", "TABERNASH", "HARDESTY", "AMAGANSETT", "PURVIS", "MARTHA", "WOODWAY", "WATFORD", "METZGER", "RICKETTS", "STEAMBOAT ROCK", "ARLINGTON HEIGHTS", "DRIPPING SPRINGS", "ZILLAH", "CARBON HILL", "SANDY VALLEY", "DUDLEY", "WATERVILLE", "ALTON NORTH", "MOORESVILLE", "PISTAKEE HIGHLANDS", "SAN LUCAS", "ALBERTVILLE", "GRAND JUNCTION", "DUNNELL", "ROWLETT", "WILSON-CONOCOCHEAGUE", "ANDOVER", "ALGONA", "ORCHARD", "VIOLA", "NAPLES", "BOARDMAN", "MIDWAY-HARDWICK", "BELFRY", "FORT LEE", "BREMEN", "NEW POST", "ELKTON", "DANUBE", "PARK RIDGE", "HOUSTON", "LAKE BUTLER", "OGDEN", "COZAD", "CALEDONIA", "GOLDEN VALLEY", "RAYLAND", "TYRONE", "KINGSTON SPRINGS", "CAROLINA", "HUNTINGTON", "BRIARCLIFFE ACRES", "ESPARTO", "ALTOONA", "EDGEWOOD", "BUTLER", "VINE HILL", "STEINAUER", "NEWPORT", "MARION", "BURBANK", "WEST GLENDIVE", "LAKE ANDES", "DAISY", "CHESTERHILL", "BRUNSWICK", "EAST HAMPTON", "UNION", "WHITEWATER", "GARNETT", "HICKSVILLE", "WYOMING", "COMSTOCK", "PORTLAND", "TROY", "ST. FLORIAN", "BASSFIELD", "LAKE ERIE BEACH", "THE VILLAGE OF INDIAN HILL", "NORTH SIOUX CITY", "OVID", "SPRAGUE", "GOLD BAR", "ROCK SPRINGS", "SEDALIA", "KINGSTOWN", "PECAN HILL", "ESTHERVILLE", "NEW HOME", "CAMBRIAN PARK", "COOKEVILLE", "BARNESTON", "REDWATER", "RIO RICO SOUTHWEST", "MUDDY", "SUCCASUNNA-KENVIL", "HALLSVILLE", "NORTH NEWTON", "SOLIS" });
            cityNamesOriginal = new List<string>(cityNames);
            cityTransforms = new List<Transform>();
            paths = new List<Tuple<Transform, Transform>>();
            
            CreateCities(Settings._numOfCities);
            CreateCompleteGraph();
            ReduceGraph();
            Display();
            SelectedCityText.text = "Selected City: ";
            Timer.DayPassed += OnDayPassed;    
        }
    
        public void Update()
        {
            if (gameStarted == false)
            {
                int startingInfection = random.Next(cityTransforms.Count - 1);
                cityTransforms[startingInfection].gameObject.GetComponent<City>().GetModel().InfectPopulation(1);
                gameStarted = true;
            }            
        }            

        public void CreateCities(int numberOfCities)
        {
            double x, y, change = 2 * Math.PI / numberOfCities;
            for (int i = 0; i < numberOfCities; i++)
            {
                var angle = i * change;

                y = Radius * Math.Sin(angle) + CentrePos.y;
                x = Radius * Math.Cos(angle) + CentrePos.x;

                var cityIndex = random.Next(cityNames.Count);
                var cityName = cityNames[cityIndex];

                var spriteTransform = InstantiateSpriteObject<City>(NodeTexture, 500f);
                spriteTransform.position = new Vector2((float)x, (float)y);
                spriteTransform.name = cityName;
                //spriteTransform.position = new Vector3(spriteTransform.position.x, spriteTransform.position.y, );
                spriteTransform.gameObject.AddComponent<BoxCollider>();
                

                cityNames.RemoveAt(cityIndex);
                cityTransforms.Add(spriteTransform);                   
            }                       
        }

        public void CreateCompleteGraph()
        {            
            for (int k = 0; k < cityTransforms.Count; k++)
            {
                for (int j = k + 1; j < cityTransforms.Count; j++)
                {
                    var p = new Tuple<Transform, Transform>(cityTransforms[k], cityTransforms[j]);                    
                    paths.Add(p);
                }
            }
        }

        public void ReduceGraph()
        {
            var numberToRemove = cityTransforms.Count - 2;                

            for (int i = 0; i < numberToRemove; i++)
            {
                var index = random.Next(1, paths.Count);
                paths.RemoveAt(index);
            }          
        }

        private void Display()
        {
            float j = 0;

            foreach (Tuple<Transform, Transform> path in paths)
            {
                var v1 = path.Item1.position;
                var v2 = path.Item2.position;
                var v1tov2 = Math.Sqrt(Math.Pow(v1.x - v2.x, 2) + Math.Pow(v1.y - v2.y, 2));
                float angle;

                if (v1.x == v2.x)
                {
                    angle = 90;
                }
                else
                {
                    angle = (float)(Math.Atan((v1.y - v2.y) / (v1.x - v2.x)) * 180 / Math.PI);
                }

                var pathTransform = InstantiateSpriteObject<Path>(PathTexture, 680f);
                pathTransform.position = new Vector2((v1.x + v2.x) / 2, (v1.y + v2.y) / 2);
                pathTransform.Rotate(0, 0, angle);

                float magnitude = pathTransform.position.magnitude;
                Vector3 newScale = new Vector3((float)v1tov2, 1, 0);

                pathTransform.localScale = newScale;
                pathTransform.gameObject.GetComponent<SpriteRenderer>().color = new Color(j, j + 0.05f, j);
                pathTransform.gameObject.AddComponent<BoxCollider>();
                pathTransform.name = path.Item1.name + " to " + path.Item2.name;

                j = j + 0.01f;
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

        public void SelectCity(string cityName)
        {
            currentSelectedCity = cityName;
        }

        public void OnDayPassed(object sender, EventArgs e)
        {
            SpreadDisease();
            UpdateCities();
            UpdateMoney();
            UpdatePopulationStats();
        }            

        private void SpreadDisease()
        {
            foreach (Tuple<Transform, Transform> path in paths)
            {
                City cityOne = path.Item1.gameObject.GetComponent<City>();
                City cityTwo = path.Item2.gameObject.GetComponent<City>();

                if (cityOne.IsDiseaseSpreading(cityTwo))
                {
                    cityTwo.GetModel().InfectPopulation(1);
                }
                else if (cityTwo.IsDiseaseSpreading(cityOne))
                {
                    cityOne.GetModel().InfectPopulation(1);

                }
            }
        }

        public void UpdateMoney()
        {
            money += income;

            float cost = 0f;
            foreach (Transform transform in cityTransforms)
            {
                City city = transform.gameObject.GetComponent<City>();
                cost += city.GetModel().GetLastCost();
            }

            money -= (int)Math.Ceiling(cost);
            MoneyText.text = "Money: $ " + money;
        }

        private void UpdateCities()
        {
            foreach (Transform c in cityTransforms)
            {
                City city = (c.gameObject.GetComponent("City") as City);
                city.GetModel().Update();
                (c.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).color =
                    new Color(1.0f, 1.0f - (float)(Math.Log10(city.GetModel().GetInfectedPopulation()) / Math.Log10(city.GetModel().GetTotalPopulation())), 1.0f - (float)(Math.Log10(city.GetModel().GetInfectedPopulation()) / Math.Log10(city.GetModel().GetTotalPopulation())));
            }
        }        

        public void UpdatePopulationStats()
        {            
            if (cityNamesOriginal.Contains(currentSelectedCity))
            {
                SelectedCityText.text = "Selected City: " + currentSelectedCity;

                foreach (var transform in cityTransforms)
                {
                    if (transform.gameObject.name == currentSelectedCity)
                    {                        
                        City city = transform.gameObject.GetComponent<City>();
                        TotalPopText.text = "Total Population: " + Math.Ceiling(city.GetModel().GetTotalPopulation());
                        InfPopText.text = "Infected Population: " + Math.Ceiling(city.GetModel().GetInfectedPopulation());
                        SusPopText.text = "Susceptible Population: " + Math.Ceiling(city.GetModel().GetSusceptiblePopulation());
                        ImmunePopText.text = "Immune Population: " + Math.Ceiling(city.GetModel().GetRecovered());                                                
                    }                    
                }
            }
        }

    }
}

