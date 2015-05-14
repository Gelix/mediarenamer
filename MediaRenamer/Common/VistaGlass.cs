/**
 * Copyright 2009 Benjamin Schirmer
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;


namespace MediaRenamer.Common {
    public class VistaGlass {
        public struct Margins {
            public Margins(Rectangle t) {
                Left = (int)t.Left;
                Right = (int)t.Right;
                Top = (int)t.Top;
                Bottom = (int)t.Bottom;
            }
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        [DllImport("dwmapi.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);

        [DllImport("dwmapi.dll")]
        static extern void DwmIsCompositionEnabled(ref bool pfEnabled);

        public static bool IsGlassSupported() {
            if (Environment.OSVersion.Version.Major < 6) {
                // Feature is only available in Vista and later
                return false;
            }

            bool isGlassSupported = false;
            DwmIsCompositionEnabled(ref isGlassSupported);
            return isGlassSupported;
        }

        public static void ExtendGlassFrame(IntPtr hwnd, ref Margins marg) {
            if (!VistaGlass.IsGlassSupported()) return;
            DwmExtendFrameIntoClientArea(hwnd, ref marg);
        }
    }
}
