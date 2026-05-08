using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        // Verify the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Create a new PDF document and bind the XML content to it
        using (Document doc = new Document())
        {
            doc.BindXml(xmlPath);

            // Set a display duration for each page (in seconds).
            // This enables a simple presentation mode where pages advance automatically.
            foreach (Page page in doc.Pages)
            {
                page.Duration = 2; // Show each page for 2 seconds
            }

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with presentation settings saved to '{outputPdf}'.");
    }
}