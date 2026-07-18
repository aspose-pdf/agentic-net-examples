using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Input XML file that will be converted to PDF
        const string xmlPath = "input.xml";
        // Output PDF file
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML into a PDF document using XmlLoadOptions
        using (Document pdfDocument = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Create optimization options and enable object stream compression
            OptimizationOptions optOptions = new OptimizationOptions
            {
                CompressObjects = true   // Pack PDF objects into streams and compress them
            };

            // Apply the optimization to the document
            pdfDocument.OptimizeResources(optOptions);

            // Save the compressed PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{pdfPath}'.");
    }
}