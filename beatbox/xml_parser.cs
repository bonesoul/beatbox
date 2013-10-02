using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace beatbox
{
    public class xml_parser
    {
        
        XmlDocument playlist = new XmlDocument();
        
        public xml_parser(string fileName) // constructor.
        {

            this.playlist.Load(fileName.ToString());
                       
        }

        public XmlNodeList get_path(int number)
        {
            XmlNodeList _path = playlist.GetElementsByTagName("path" + number);

            return _path;

        }

        public XmlNode get_color(XmlNodeList path)
        {
            return path.Item(0);

        }

        public string get_filename(XmlNodeList path)
        {
            return path.Item((path.Count) - 1).ToString();
        }


        public List<XmlNode> get_startTime(XmlNodeList path)
        {
            List<XmlNode> start_time_list = new List<XmlNode>();
            for (int i = 2; i < path.Count; i++)
            {
                start_time_list.Add(path.Item(i));

            }

            return start_time_list;
        }

        List<Path> PathList = new List<Path>();


    }
}
