using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputXmlPath = "input.xml";
        const string outputPdfPath = "compressed_output.pdf";

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputXmlPath}");
            return;
        }

        // Load the XML representation of a PDF document
        using (Document pdfDoc = new Document())
        {
            pdfDoc.BindXml(inputXmlPath);

            // Configure optimization to compress PDF objects into streams
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
            };

            // Apply the optimization strategy to the document resources
            pdfDoc.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPdfPath}'.");
    }
}