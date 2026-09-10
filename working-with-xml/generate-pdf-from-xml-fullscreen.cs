using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML and the resulting PDF
        const string xmlPath   = "input.xml";
        const string pdfPath   = "output.pdf";

        // Verify the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML using the proper load options
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Use the recommended using pattern for deterministic disposal
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Start the document in full‑screen mode when opened
            doc.PageMode = PageMode.FullScreen;

            // Define how the document should be displayed after exiting full‑screen
            doc.NonFullScreenPageMode = PageMode.UseNone; // e.g., no UI components

            // Optional: resize the window to fit the first page
            doc.FitWindow = true;

            // Save the PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with full‑screen viewer mode: {pdfPath}");
    }
}