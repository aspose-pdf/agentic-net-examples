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

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML using the appropriate load options
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Create the PDF document from the XML
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Enable full‑screen viewer mode when the PDF is opened
            doc.PageMode = PageMode.FullScreen;

            // Define how the document should be displayed after exiting full‑screen (optional)
            doc.NonFullScreenPageMode = PageMode.UseNone;

            // Save the PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with full‑screen mode: {pdfPath}");
    }
}