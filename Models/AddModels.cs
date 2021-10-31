using System.Reflection.Metadata;
namespace Code.Models
{
    public class AddRequest
    {
        public string name;
        public string phone;
        public string ip;
        public int numOfPass;

    }

    public class Response1
    {
        public string name;
        public string phone;
    }

    public class Response2
    {
        public string city;
    }

}