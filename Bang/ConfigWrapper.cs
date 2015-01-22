using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

namespace test3
{
    public static class ConfigWrapper
    {
        public const string DEFAULT_NAME = "config.xml";
        public const string DElIMITER = "/";
        public const string TMP_FILE = "tmp.file";

        private static string ReadValue(string settingPath, string fileName)
        {
            string result = "";
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                string[] path = Regex.Split(settingPath, DElIMITER);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                reader.MoveToContent();

                int i = 0;
                while (i < (path.Length) && reader.ReadToDescendant(path[i]))
                {
                    if (i == (path.Length - 1))
                          result = reader.ReadElementContentAsString();
                    i++;
                }
                if (reader != null)
                    reader.Close();
            }
            result = result.Trim();
            return result;
        }

        private static void WriteValue(string settingPath, string newValue, string fileName)
        {
            try
            {
                if (File.Exists(TMP_FILE))
                    File.Delete(TMP_FILE);
                
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode root = doc.DocumentElement;

                XmlNode goal = root.SelectSingleNode(settingPath);
                if (goal!= null)
                {
                    goal.InnerText = newValue;
                }
                doc.Save(TMP_FILE);

                if (File.Exists(TMP_FILE))
                {
                    File.Delete(fileName);
                    File.Move(TMP_FILE, fileName);
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static string[] ReadtAttributes(string settingPath, string fileName)
        {
            string[] result = null;           
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                string[] path = Regex.Split(settingPath, DElIMITER);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                reader.MoveToContent();

                int i = 0,j=0;
                while (i < (path.Length) && reader.ReadToDescendant(path[i]))
                {
                    if (i == (path.Length - 1))
                    {
                        if (reader.HasAttributes)
                        {
                            result = new string[reader.AttributeCount];
                            while (reader.MoveToNextAttribute())
                            {
                                result[j++] = reader.Value;
                            }
                        }
                    }
                    i++;
                }
                if (reader != null)
                    reader.Close();
            }
            return result;
        }

        // reading elevent value, work only for nodes without children!
        public static string ReadElementValue(string settingPath)
        {
            return ReadValue(settingPath, DEFAULT_NAME);
        }
        public static string ReadElementValue(string settingPath,string fileName)
        {
            return ReadValue(settingPath, fileName);
        }

        // reading elevent attributes, work only for nodes without children!
        public static string[] ReadElementAttributes(string settingPath)
        {
            return ReadtAttributes(settingPath, DEFAULT_NAME);
        }
        public static string[] ReadElementAttributes(string settingPath, string fileName)
        {
            return ReadtAttributes(settingPath, fileName);
        }

        // writing elevent value, work only for nodes without children!
        public static void WriteElementValue(string settingPath, string newValue)
        {
            WriteValue(settingPath, newValue, DEFAULT_NAME);
        }
        public static void WriteElementValue(string settingPath, string newValue, string fileName)
        {
            WriteValue(settingPath, newValue, fileName);
        }

    }
} 