using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "layout.xml";
        const string pdfPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML layout file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load XML layout and read the orientation attribute
        XDocument layoutDoc = XDocument.Load(xmlPath);
        // Expected format: <Layout orientation="landscape"/> (default to portrait)
        string orientation = (string)layoutDoc.Root.Attribute("orientation") ?? "portrait";
        bool isLandscape = string.Equals(orientation, "landscape", StringComparison.OrdinalIgnoreCase);

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Apply the orientation to every page in the document
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                pdfDoc.Pages[i].PageInfo.IsLandscape = isLandscape;
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with {(isLandscape ? "landscape" : "portrait")} orientation to '{outputPath}'.");
    }
}