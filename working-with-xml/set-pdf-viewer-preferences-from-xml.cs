using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML and the resulting PDF
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        // Ensure the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML using XmlLoadOptions (required for XML import)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Viewer preference: resize the window to fit the first displayed page
            doc.FitWindow = true;

            // Optional: set the page mode (e.g., no side panels)
            doc.PageMode = PageMode.UseNone;

            // Save the document as PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with viewer preferences: {pdfPath}");
    }
}