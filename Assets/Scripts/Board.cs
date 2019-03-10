using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace healthHack
{
    public class Board : MonoBehaviour
    {
        private System.Random random;
        
        private List<City> cities;
        private List<Tuple<City, City>> paths;
        private List<string> cityNames;
        public int numberOfCities;

        public Texture2D nodeTexture;
        public Texture2D pathTexture;
        public Vector2 centrePos;
        public double radius;
        

        void Start()
        {            
            random = new System.Random();
            cityNames = new List<string>(new string[] { "ETHEL", "LAUREL", "TABERNASH", "HARDESTY", "AMAGANSETT", "PURVIS", "MARTHA", "WOODWAY", "METZGER", "RICKETTS", "STEAMBOAT ROCK", "ARLINGTON HEIGHTS", "DRIPPING SPRINGS", "ZILLAH", "CARBON HILL", "SANDY VALLEY", "DUDLEY", "WATERVILLE", "ALTON NORTH", "MOORESVILLE", "PISTAKEE HIGHLANDS", "SAN LUCAS", "ALBERTVILLE", "GRAND JUNCTION", "DUNNELL", "ROWLETT", "WILSON-CONOCOCHEAGUE", "ANDOVER", "ALGONA", "ORCHARD", "VIOLA", "NAPLES", "BOARDMAN", "MIDWAY-HARDWICK", "BELFRY", "FORT LEE", "BREMEN", "NEW POST", "ELKTON", "DANUBE", "PARK RIDGE", "HOUSTON", "LAKE BUTLER", "OGDEN", "COZAD", "CALEDONIA", "GOLDEN VALLEY", "RAYLAND", "TYRONE", "KINGSTON SPRINGS", "CAROLINA", "HUNTINGTON", "BRIARCLIFFE ACRES", "ESPARTO", "ALTOONA", "EDGEWOOD", "BUTLER", "VINE HILL", "STEINAUER", "NEWPORT", "MARION", "BURBANK", "WEST GLENDIVE", "LAKE ANDES", "DAISY", "CHESTERHILL", "BRUNSWICK", "EAST HAMPTON", "UNION", "WHITEWATER", "GARNETT", "HICKSVILLE", "WYOMING", "COMSTOCK", "PORTLAND", "TROY", "ST. FLORIAN", "BASSFIELD", "LAKE ERIE BEACH", "THE VILLAGE OF INDIAN HILL", "NORTH SIOUX CITY", "OVID", "SPRAGUE", "GOLD BAR", "ROCK SPRINGS", "SEDALIA", "KINGSTOWN", "PECAN HILL", "ESTHERVILLE", "NEW HOME", "CAMBRIAN PARK", "COOKEVILLE", "BARNESTON", "REDWATER", "RIO RICO SOUTHWEST", "MUDDY", "SUCCASUNNA-KENVIL", "HALLSVILLE", "NORTH NEWTON", "SOLIS" });
            
            cities = new List<City>();
            paths = new List<Tuple<City, City>>();

            numberOfCities = Difficulty.GetNumOfCities();
            Debug.Log(Difficulty.GetNumOfCities());

            CreateCities(numberOfCities);
            CreateCompleteGraph();
            ReduceGraph();
            
            
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

                var button = spriteTransform.gameObject.AddComponent<Button>();
                button.onClick.AddListener(TaskOnClick);
                

                spriteTransform.position = new Vector2((float)x, (float)y);
                spriteTransform.name = cityName;
                
                cityNames.RemoveAt(cityIndex);
            }           
        }

        public void TaskOnClick()
        {
            Debug.Log("You have clicked the button!");
        }

        public void CreateCompleteGraph()
        {
            for (int k = 0; k < cities.Count; k++)
            {
                for (int j = k + 1; j < cities.Count; j++)
                {
                    paths.Add(new Tuple<City, City>(cities[k], cities[j]));
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
    }
}

