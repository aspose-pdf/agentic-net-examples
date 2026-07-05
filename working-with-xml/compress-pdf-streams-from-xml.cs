using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "compressed_output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlPath}");
            return;
        }

        // Create a new PDF document and bind the XML content.
        using (Document pdfDoc = new Document())
        {
            pdfDoc.BindXml(xmlPath);

            // Set up optimization to compress PDF objects into streams.
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
            };

            // Apply the optimization strategy.
            pdfDoc.OptimizeResources(opt);

            // Save the compressed PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPdf}'.");
    }
}