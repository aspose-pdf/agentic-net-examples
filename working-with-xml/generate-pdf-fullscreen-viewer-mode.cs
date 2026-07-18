using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to input XML and output PDF
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML with proper load options, then set viewer preferences
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Open the document in full‑screen mode
            doc.PageMode = PageMode.FullScreen;

            // Optional: define how the document behaves after exiting full‑screen
            doc.NonFullScreenPageMode = PageMode.UseNone;

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with full‑screen mode: {pdfPath}");
    }
}