using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Pizza Hutt", Cuisine = CuisineType.Italian},
                new Restaurant { Id = 2, Name = "Lasania", Cuisine = CuisineType.Indian},
                new Restaurant { Id = 3, Name = "Pindi Khokha", Cuisine = CuisineType.French},
            };
        }

        public void Add(Restaurant restaurant)
        {
            restaurants.Add(restaurant);
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
        }
        public void Update(Restaurant restaurant)
        {
            int restaurantIndex = restaurants.FindIndex(e => e.Id == restaurant.Id);

            if(restaurantIndex == -1)
            {
                throw new Exception (
                    
                    string.Format("Unable to find an entry with an ID of {0}", restaurant.Id));
            }
            restaurants[restaurantIndex] = restaurant;
        }

        public Restaurant Get(int id)
        {
            return restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants.OrderBy(r => r.Name);
        }

        
    }
}
