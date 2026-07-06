using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

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

        // Load the XML content into a PDF document using XmlLoadOptions
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Enable compression of PDF objects (including text streams)
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
            };
            doc.OptimizeResources(opt);

            // Save the compressed PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{pdfPath}'.");
    }
}