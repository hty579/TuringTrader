﻿//==============================================================================
// Project:     TuringTrader, simulator core
// Name:        PlotterRenderCSharp
// Description: Plotter renderer for C# templates
// History:     2019vi20, FUB, created
//------------------------------------------------------------------------------
// Copyright:   (c) 2011-2018, Bertram Solutions LLC
//              http://www.bertram.solutions
// License:     This code is licensed under the term of the
//              GNU Affero General Public License as published by 
//              the Free Software Foundation, either version 3 of 
//              the License, or (at your option) any later version.
//              see: https://www.gnu.org/licenses/agpl-3.0.en.html
//==============================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TuringTrader.Simulator;

#if false
#region private void OpenWithCSharp(string pathToCSharpTemplate)
#if ENABLE_CSHARP
        private void OpenWithCSharp(string pathToCSharpTemplate)
        {
            void uiThread()
            {
                //----- dynamic compile
                var assy = DynamicCompile.CompileSource(pathToCSharpTemplate);

                if (assy == null)
                {
                    Output.WriteLine("Plotter: can't compile template {0}", pathToCSharpTemplate);
                    return;
                }

                //----- instantiate template
                var templateType = assy.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(ReportTemplate)))
                    .FirstOrDefault();

                if (templateType == null)
                {
                    Output.WriteLine("Plotter: can't load template {0}", pathToCSharpTemplate);
                    return;
                }

                ReportTemplate template = (ReportTemplate)Activator.CreateInstance(templateType);
                template.PlotData = AllData;

                //----- open dialog
                var report = new Report(template);
                report.ShowDialog();
            }

            // The calling thread must be STA, because many UI components require this.
            // https://stackoverflow.com/questions/2329978/the-calling-thread-must-be-sta-because-many-ui-components-require-this

            Thread thread = new Thread(uiThread);
            thread.SetApartmentState(ApartmentState.STA);

            thread.Start();
            thread.Join();
        }
#else
private void OpenWithCSharp(string pathToCSharpTemplate)
{
    Output.WriteLine("Plotter: OpenWithCSharp bypassed w/ ENABLE_CSHARP switch");
}
#endif
#endregion
#endif

namespace TuringTrader
{
    static class PlotterRenderCSharp
    {
        public static void Register()
        {
            Plotter.Renderer += Renderer;
        }

        public static void Renderer(Plotter plotter, string template)
        {
            if (Path.GetExtension(template).ToLower() != ".cs")
                return;
        }
    }
}

//==============================================================================
// end of file