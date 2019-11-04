using System;

namespace DATINGAPP.API.Dtos
{
    public class PhotoForDetailDto
    {
          public int Id { get; set; }
        public string Url { get; set; }
        public string Desciption { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
       
    }
}