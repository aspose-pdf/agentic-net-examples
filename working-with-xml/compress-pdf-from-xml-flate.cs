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

        // Load the XML file and create a PDF document from it
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Configure optimization to compress PDF objects (Flate compression)
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
            };

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the compressed PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{pdfPath}'.");
    }
}