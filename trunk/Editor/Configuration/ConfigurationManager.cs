using System.IO;
using System.Runtime.InteropServices;
using System.Text;

/*
 * http://www.codeproject.com/KB/cs/cs_ini.aspx
 * Created by 
 * BLaZiNiX
 * Web Developer
 * Canada
 * 
 * Adapted for usage by Rosthouse
 */

namespace Editor.Configuration
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    public class IniFile
    {
        private static string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        private static IniFile instance;

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath">The path to the ini file</PARAM>
        /// <remarks>
        /// If the file can't be found, a new ini file will be created
        /// </remarks>
        private IniFile(string INIPath)
        {
            //We need a file to work with, so we just create one if we can't find one
            //Rosthouse
            if(!File.Exists(INIPath))
            {
                File.CreateText(INIPath);
            }

            path = INIPath;
        }

        public static IniFile Configuration
        {
            get
            {
                if(instance == null)
                {
                    if(path != string.Empty)
                    {
                        path = "Configuration.ini";
                    }

                    instance = new IniFile(path);
                }

                return instance;
            }
        }

        public static void SetConfigurationPath(string IniPath)
        {
            instance = new IniFile(IniPath);
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section">Section name</PARAM>
        /// <PARAM name="Key">Key Name</PARAM>
        /// <PARAM name="Value">Value</PARAM>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section">Section Name</PARAM>
        /// <PARAM name="Key">Key name</PARAM>
        /// <returns>The value of the key</returns>

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, path);
            return temp.ToString();

        }
    }
}