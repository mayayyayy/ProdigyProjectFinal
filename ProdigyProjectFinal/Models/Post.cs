using System;
using System.Collections.Generic;
using System.Linq;
using ProdigyProjectFinal.Services;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.Models
{
    public class Post
    {
        //private readonly List<string> imageTypes = new() { ".jpg", ".jfif", ".png", ".gif" };

        //public int Id { get; set; }



        //public DateTime UploadDateTime { get; set; } = DateTime.Now;

        //public string FileExtension { get; set; }
        //public User Creator { get; set; }

       
        //[JsonIgnore] public string File => string.IsNullOrEmpty(FileExtension) ? "" : $"{ProdigyServices.WwwRoot}/{Id}{FileExtension}";
        //[JsonIgnore] public bool IsFile => !string.IsNullOrEmpty(FileExtension);
        //[JsonIgnore] public bool IsImage => imageTypes.Contains(FileExtension);
       

        //[JsonIgnore]
        //public string DateTimeString
        //{
        //    get
        //    {
        //        if (UploadDateTime.Date == DateTime.Now.Date)
        //            return UploadDateTime.ToString("HH:mm");

        //        if (UploadDateTime.Year == DateTime.Now.Year)
        //            return UploadDateTime.ToString("dd/MM");

        //        return UploadDateTime.ToString("dd/MM/yyyy");
        //    }
        //}

    }
}

