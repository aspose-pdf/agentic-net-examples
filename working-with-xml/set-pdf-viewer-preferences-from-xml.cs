using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input XML and output PDF
        const string xmlPath   = "input.xml";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {xmlPath}");
            return;
        }

        // Load XML with explicit XmlLoadOptions (required for XML import)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Use the recommended lifecycle pattern: wrap Document in a using block
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Viewer preferences -------------------------------------------------
            // Resize the viewer window to fit the first displayed page
            doc.FitWindow = true;

            // Optional: center the window on the screen when opened
            doc.CenterWindow = true;

            // Optional: hide UI elements for a cleaner view
            doc.HideMenubar   = false;
            doc.HideToolBar   = false;
            doc.HideWindowUI  = false;

            // Optional: set the initial page mode (e.g., no outlines)
            doc.PageMode = PageMode.UseNone;

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with viewer preferences: {pdfPath}");
    }
}