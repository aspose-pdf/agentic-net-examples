using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML file. XmlLoadOptions can be used to supply an XSL stylesheet
        // if the XML contains processing instructions that affect conversion.
        // Here we use the default constructor (no XSL) and rely on any
        // processing instructions present in the XML itself.
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Example of influencing page size based on XML instructions.
            // In a real scenario you would parse the XML for custom instructions.
            // For demonstration we set the first page to A4 size (595x842 points).
            if (doc.Pages.Count > 0)
            {
                // SetPageSize expects width and height in points (1 point = 1/72 inch).
                doc.Pages[1].SetPageSize(595, 842);
            }

            // Save the resulting PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated and saved to '{outputPdf}'.");
    }
}