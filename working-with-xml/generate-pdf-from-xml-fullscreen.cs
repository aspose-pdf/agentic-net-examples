using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file using the appropriate load options
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Ensure the Document is disposed properly
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Open the PDF in full‑screen mode when the viewer starts
            doc.PageMode = PageMode.FullScreen;

            // Define how the document should be displayed after exiting full‑screen
            doc.NonFullScreenPageMode = PageMode.UseOutlines; // optional, adjust as needed

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with full‑screen viewer mode: {pdfPath}");
    }
}