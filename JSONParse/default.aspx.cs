using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using Newtonsoft.Json;

namespace JSONParse
{

    public class Medium
    {
        public string MediaType { get; set; }
        public int MediaId { get; set; }
        public string MediaUrl { get; set; }
        public string MediaUrlLarge { get; set; }
        public string MediaUrlSmall { get; set; }
    }

    public class Crew
    {

        public string CrewId { get; set; }
        public string CrewName { get; set; }
        public string CrewDescription { get; set; }
        public string CrewTitle { get; set; }
        public string CrewSince { get; set; }
        public List<Medium> Media { get; set; }
    }

    public class RootObjectCrew
    {
        public int RequestValid { get; set; }
        public int ItemsReturned { get; set; }
        public List<Crew> Crew { get; set; }
        public long RequestCachedTicks { get; set; }
        public int RequestCached { get; set; }
    }


    public class Driver
    {
        public string DriverId { get; set; }
        public string DriverFName { get; set; }
        public string DriverLName { get; set; }
        public string DriverSuffix { get; set; }
        public string DriverCity { get; set; }
        public string DriverState { get; set; }
        public string DriverCountry { get; set; }
    }

    public class Point
    {
        public int Position { get; set; }
        public string CarNum { get; set; }
        public string PointTotal { get; set; }
        public int Wins { get; set; }
        public int Top5 { get; set; }
        public int Top10 { get; set; }
        public int Features { get; set; }
        public List<Driver> Driver { get; set; }
    }

    public class RootObjectPoints
    {
        public int RequestValid { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassNameOverride { get; set; }
        public string PointsAsOf { get; set; }
        public List<Point> Points { get; set; }
        public long RequestCachedTicks { get; set; }
        public int RequestCached { get; set; }
    }

    public partial class _default : System.Web.UI.Page
    {
        //Instructions
        //Consume JSON data from supplied API with API Key and display as HTML on the page.  
        //Use Newtonsoft.Json for JSON parsing (I've included the library and reference in this page)
        //------You may have to fix the referance to the file located in the "Libs" folder of this solution.

        //**DONT WORRY ABOUT HTML STYLE**

        

        protected void Page_Load(object sender, EventArgs e)
        {
            loadCrewData();
            loadPointsData();
        }

        private void loadCrewData()
        {
            WebClient client = new WebClient();
            string crewdata = client.DownloadString("https://api.myracepass.com/v2/crew/?key=0524cf43-ec2d-4c7e-b5bc-fba209f33051");
            
            RootObjectCrew crew = new RootObjectCrew();
            //Crew medium = new Crew();
            
            RootObjectCrew deserializedcrew = JsonConvert.DeserializeObject<RootObjectCrew>(crewdata);
            //Crew deserializedmedium = JsonConvert.DeserializeObject<Crew>(crewdata);

            List<string> crewlist = new List<string>();

            List<Crew> decrew = deserializedcrew.Crew;
            //List<Medium> demedium = deserializedmedium.Media;

            string HTMLstr = "";

            HTMLstr = HTMLstr + " <table border = \"1\"> ";


            //Create the "Header/Name Row"
            HTMLstr = HTMLstr + " <tr> ";
            HTMLstr = HTMLstr + " <td> " + " CrewName " + " </td> ";
            HTMLstr = HTMLstr + " <td> " + " CrewTitle " + " </td> ";
            HTMLstr = HTMLstr + " <td> " + " Image " + " </td> ";
            HTMLstr = HTMLstr + " </tr> ";

            for (int i = 0; i < decrew.Count; i++)

            {
                string tmpstr = "";

                HTMLstr = HTMLstr + " <tr> ";
                tmpstr = tmpstr + " <td> " + decrew[i].CrewName + " </td> ";
                tmpstr = tmpstr + " <td> " + decrew[i].CrewTitle + " </td> ";
                tmpstr = tmpstr + " <td> <a href=\"" + decrew[i].Media.FirstOrDefault().MediaUrl + "\"> " 
                    + decrew[i].Media.FirstOrDefault().MediaUrl + " </a>  </td> ";
                HTMLstr = HTMLstr + tmpstr;
                HTMLstr = HTMLstr + " </tr> ";

                crewlist.Add(tmpstr);
            };


            HTMLstr = HTMLstr + " </table> ";


            //string CrewInfo = deserializedcrew.Crew[0].CrewName + "<br />" + deserializedcrew.Crew[0].CrewTitle + "<br />" + deserializedcrew.Crew[0].Media[0].MediaUrlLarge;


            //SAMPLE 1
            //API documentation is located here: http://www.myracepass.com/developers/api/?c=1221&i=17444
            //API endpoint: https://api.myracepass.com/v2/crew/?key=0524cf43-ec2d-4c7e-b5bc-fba209f33051
            //Display the crem members name, title, and image.

            //TODO - Make API request and parse data


            litCrewHTML.Text = HTMLstr;

            //foreach (string crewlistline in crewlist)
            //{
            //    litCrewHTML.Text = crewlistline;
            //}

            //litCrewHTML.Text = CrewInfo;
        }

        private void loadPointsData()
        {
            WebClient client = new WebClient();
            string pointsdata = client.DownloadString("https://api.myracepass.com/v2/points/?key=0524cf43-ec2d-4c7e-b5bc-fba209f33051&ClassId=1002&ScheduleId=4775");

            RootObjectPoints crew = new RootObjectPoints();

            RootObjectPoints deserializedpoints = JsonConvert.DeserializeObject<RootObjectPoints>(pointsdata);

            List<string> pointlist = new List<string>();

            List<Point> depoint = deserializedpoints.Points;

            //for (int i = 0; i < .Count; i++)
            string HTMLstr2 = "";

            HTMLstr2 = HTMLstr2 + " <table border = \"1\"> ";


            //Create the "Header/Name Row"
            HTMLstr2 = HTMLstr2 + " <tr> ";
            HTMLstr2 = HTMLstr2 + " <td> " + " Position " + " </td> ";
            HTMLstr2 = HTMLstr2 + " <td> " + " First Name " + " </td> ";
            HTMLstr2 = HTMLstr2 + " <td> " + " Last Name " + " </td> ";
            HTMLstr2 = HTMLstr2 + " <td> " + " Wins " + " </td> ";
            HTMLstr2 = HTMLstr2 + " <td> " + " Total Points " + " </td> ";
            HTMLstr2 = HTMLstr2 + " </tr> ";

            for (int i = 0; i < depoint.Count; i++)

            {
                string tmpstr = "";
               
                HTMLstr2 = HTMLstr2 + " <tr> ";
                tmpstr = tmpstr + " <td> " + depoint[i].Position + " </td> ";
                tmpstr = tmpstr + " <td> " + depoint[i].Driver.FirstOrDefault().DriverFName + " </td> ";
                tmpstr = tmpstr + " <td> " + depoint[i].Driver.FirstOrDefault().DriverLName + " </td> ";
                tmpstr = tmpstr + " <td> " + depoint[i].Wins + " </td> ";
                tmpstr = tmpstr + " <td> " + depoint[i].PointTotal + " </td> ";
                HTMLstr2 = HTMLstr2 + tmpstr;
                HTMLstr2 = HTMLstr2 + " </tr> ";

                pointlist.Add(tmpstr);
            };


            HTMLstr2 = HTMLstr2 + " </table> ";

            



            //for

            //SAMPLE 2
            //API documentation is located here: http://www.myracepass.com/developers/api/?c=1222&i=17496
            //API endpoint: https://api.myracepass.com/v2/points/?key=0524cf43-ec2d-4c7e-b5bc-fba209f33051&ClassId=1002&ScheduleId=4775
            //Display a table of Position, First Name, Last Name, Wins, and Total Points

            //TODO - Make API request and parse data


            litPointsHTML.Text = HTMLstr2;
        }
    }
}