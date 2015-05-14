using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRenamer.Common {
    public abstract class ParserBase {
        // Valid extensions
        internal String[] extension = {
            ".avi", ".mkv", ".mpg", ".mpeg", ".mov", ".wmv",
			".rpm", ".ogm", ".srt", ".sub", ".mp4", ".divx",
            ".idx", ".rar"};

        internal String scanPath = "";

        public ParserBase(String path) {
            if (!path.EndsWith(@"\")) path += @"\";
            scanPath = path;
        }

        abstract internal void scanFolder(String folder);

        public void startScan() {
            if (scanPath == "")
                return;
            scanFolder(scanPath);
        }

        public event ScanProgressHandler ScanProgress;
        protected virtual void OnScanProgress(int pos, int max) {
            ScanProgressHandler handler = ScanProgress;
            if (handler != null) {
                handler.Invoke(pos, max);
            }
        }

        public event ScanDone ScanDone;
        protected virtual void OnScanDone() {
            ScanDone handler = ScanDone;
            if (handler != null) {
                handler.Invoke();
            }
        }

        /// <summary>
        /// Checks for valid extensions
        /// </summary>
        /// <param name="name">filename</param>
        /// <returns></returns>
        internal bool isValidExt(String name) {
            name = name.ToLower();
            foreach (String ext in extension) {
                if (name.EndsWith(ext)) return true;
            }
            return false;
        }
        
    }
}
