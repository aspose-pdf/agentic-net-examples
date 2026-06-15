using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input XML and output PDF
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {xmlPath}");
            return;
        }

        // Load the XML file using the correct load options
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Create the PDF document from XML
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Enable full‑screen viewer mode when the PDF is opened
            doc.PageMode = PageMode.FullScreen;

            // Define how the document should be displayed after exiting full‑screen
            doc.NonFullScreenPageMode = PageMode.UseOutlines;

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with full‑screen mode: {pdfPath}");
    }
}