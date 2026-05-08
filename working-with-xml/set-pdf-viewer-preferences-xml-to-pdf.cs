using System;
using System.IO;
using Aspose.Pdf; // Core API (Document, PageMode, XmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input XML file that will be converted to PDF
        const string xmlPath = "input.xml";

        // Output PDF file
        const string pdfPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {xmlPath}");
            return;
        }

        // Load the XML using XmlLoadOptions (required for XML → PDF conversion)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Use a using block for deterministic disposal of the Document
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Viewer preference: resize the window to fit the first displayed page
            pdfDocument.FitWindow = true;

            // Optional: set the initial page mode (e.g., show outlines)
            pdfDocument.PageMode = PageMode.UseOutlines;

            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with viewer preferences: {pdfPath}");
    }
}
