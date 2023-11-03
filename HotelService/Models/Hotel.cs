﻿namespace HotelService.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int StarRating { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
