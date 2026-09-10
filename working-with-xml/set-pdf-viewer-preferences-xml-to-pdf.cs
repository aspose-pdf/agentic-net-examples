using System;
using System.IO;
using Aspose.Pdf; // Core API – load option classes (e.g., XmlLoadOptions) are in this namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file with default XmlLoadOptions (no extra using needed)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Document creation – wrapped in using for deterministic disposal
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Viewer preferences -------------------------------------------------
            // Resize the PDF viewer window to fit the first displayed page
            doc.FitWindow = true;

            // Set the page mode (how the document is displayed when opened)
            // UseNone means no side panels (outlines, thumbnails, etc.) are shown
            doc.PageMode = PageMode.UseNone;
            // ---------------------------------------------------------------------

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with viewer preferences: {pdfPath}");
    }
}
