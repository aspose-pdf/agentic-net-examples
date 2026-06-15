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

        // Load the XML content into a PDF document using the correct load options
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Configure optimization to compress PDF objects (including text streams)
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
            };

            // Apply the optimization strategy
            doc.OptimizeResources(opt);

            // Save the optimized PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{pdfPath}'.");
    }
}