using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Board.Service
{
    public class Developer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int ID { get; set; }
    }

    public class Package
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public int Likes { get; set; }
        public Developer Developer { get; set; }
        public string Version { get; set; }
        string _Icon;

        public dynamic Icon
        {
            get
            {
                byte[] Base64Stream = Convert.FromBase64String(this._Icon);
                return ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            }

            set { this._Icon = value; }
        }
        public string Created_At { get; set; }
    }

    public class Result
    {
        public string Message { get; set; }
        public int Status { get; set; }
    }
}
