using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MediaRenamer {
    partial class AboutBox : Form {
        public AboutBox() {
            InitializeComponent();

            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            DateTime dt = DateCompiled();

            this.Text = AssemblyTitle;
            this.labelProductName.Text = Application.ProductName + " " + v.ToString();
            this.labelWebpage.Text = "Project webpage: http://code.google.com/p/mediarenamer/";
            this.labelCopyright.Text = "Source Code licensed under Apache 2.0";
            this.labelCompiledDate.Text = "Compiled on " + dt.ToShortDateString();

            String nl = "\r\n";
            String str = "";
            str += "Data provided by:" + nl;
            str += "* EpisodeWorld.com" + nl;
            str += "* TheTVDB.com" + nl;
            str += "* IMDB.com" + nl;
            str += "* TheMovieDB.org" + nl;
            str += nl;
            str += "Third party components:" + nl;
            str += "* JsonExSerializer for C# (http://code.google.com/p/jsonexserializer/)" + nl;
            str += "* DotNetZip Library (http://www.codeplex.com/DotNetZip)" + nl;

            this.textBoxDescription.Text = str;
        }

        #region Assemblyattributaccessoren

        public string AssemblyTitle {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "") {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private DateTime DateCompiled() {
            // Assumes that in AssemblyInfo.cs,
            // the version is specified as 1.0.* or the like,
            // with only 2 numbers specified;
            // the next two are generated from the date.
            // This routine decodes them.
            Version v = Assembly.GetExecutingAssembly().GetName().Version;

            // v.Build is days since Jan. 1, 2000
            // v.Revision*2 is seconds since local midnight
            // (NEVER daylight saving time)

            DateTime t = new DateTime(
                v.Build * TimeSpan.TicksPerDay +
                v.Revision * TimeSpan.TicksPerSecond * 2
                ).AddYears(1999).AddDays(-1);

            return t;
        }

    }
}
