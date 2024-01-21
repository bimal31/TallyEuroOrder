using System.Net;
namespace OrderApp
{
    public class WSResponseObject
    {
        //public WSResponseObject();
        public string Message { get; set; }
        public int status { get; set; }
        public dynamic total_records { get; set; }
        public dynamic Data { get; set; }


    }

    public class WSResponseObjecENcryotDecrpt
    {
        //public WSResponseObject();

        public string ServiceName { get; set; }
        public WSResponseObject Parameters { get; set; }
    }
}