using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Stub implementations for missing Aspose.Pdf.Facades types (remove when the real
// Aspose.Pdf library is referenced).
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    // Minimal stub for PdfViewerPreferences to satisfy compilation.
    public class PdfViewerPreferences
    {
        private string _pdfPath;
        public void BindPdf(string pdfPath) => _pdfPath = pdfPath;
        public ViewerPreferencesInfo GetViewerPreferences() => new ViewerPreferencesInfo();
    }

    // Minimal stub for the information object returned by GetViewerPreferences().
    public class ViewerPreferencesInfo
    {
        public bool HideToolbar { get; set; }
        public bool HideMenubar { get; set; }
        public bool HideWindowUI { get; set; }
        public bool FitWindow { get; set; }
        public bool CenterWindow { get; set; }
        public bool DisplayDocTitle { get; set; }
        public NonFullScreenPageMode NonFullScreenPageMode { get; set; } = NonFullScreenPageMode.UseNone;
        public Direction Direction { get; set; } = Direction.L2R;
        public PrintScaling PrintScaling { get; set; } = PrintScaling.None;
        public Duplex Duplex { get; set; } = Duplex.Simplex;
        public bool PickTrayByPDFSize { get; set; }
        public string PrintPageRange { get; set; }
    }

    // Enumerations used by ViewerPreferencesInfo (stubbed with common values).
    public enum NonFullScreenPageMode { UseNone, UseOutlines, UseThumbs, UseOC }
    public enum Direction { L2R, R2L }
    public enum PrintScaling { None, AppDefault }
    public enum Duplex { Simplex, DuplexFlipLongEdge, DuplexFlipShortEdge }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputXml = "viewer_preferences.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the viewer preferences facade
        PdfViewerPreferences viewer = new PdfViewerPreferences();
        viewer.BindPdf(inputPath);

        // Retrieve current viewer preferences
        var prefs = viewer.GetViewerPreferences();

        // Build XML representation of the preferences
        XDocument xml = new XDocument(
            new XElement("ViewerPreferences",
                new XElement("HideToolbar", prefs.HideToolbar),
                new XElement("HideMenubar", prefs.HideMenubar),
                new XElement("HideWindowUI", prefs.HideWindowUI),
                new XElement("FitWindow", prefs.FitWindow),
                new XElement("CenterWindow", prefs.CenterWindow),
                new XElement("DisplayDocTitle", prefs.DisplayDocTitle),
                new XElement("NonFullScreenPageMode", prefs.NonFullScreenPageMode.ToString()),
                new XElement("Direction", prefs.Direction.ToString()),
                new XElement("PrintScaling", prefs.PrintScaling.ToString()),
                new XElement("Duplex", prefs.Duplex.ToString()),
                new XElement("PickTrayByPDFSize", prefs.PickTrayByPDFSize),
                new XElement("PrintPageRange", prefs.PrintPageRange ?? string.Empty)
            )
        );

        // Save the XML to a file for legacy integration
        xml.Save(outputXml);
        Console.WriteLine($"Viewer preferences saved to '{outputXml}'.");
    }
}
