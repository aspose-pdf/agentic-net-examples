using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF generated from XML
        const string outputPdf = "output_compressed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure optimization: compress objects into streams
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
                // other options remain at their defaults
            };

            // Apply the optimization to the document resources
            pdfDoc.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPdf}'.");
    }
}