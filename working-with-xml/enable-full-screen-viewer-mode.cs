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

        // Load XML into a new PDF document using the constructor that accepts XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Enable full‑screen viewer mode when the PDF is opened
            doc.PageMode = PageMode.FullScreen;

            // Define how the document should be displayed after exiting full‑screen
            doc.NonFullScreenPageMode = PageMode.UseNone;

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}' with full‑screen mode enabled.");
    }
}
