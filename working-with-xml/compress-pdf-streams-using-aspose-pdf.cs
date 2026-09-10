using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "compressed_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF (e.g., generated from XML)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure optimization: compress objects and clean up unused resources
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects      = true,
                RemoveUnusedObjects  = true,
                RemoveUnusedStreams  = true,
                LinkDuplicateStreams = true
            };

            // Apply the optimization strategy
            pdfDoc.OptimizeResources(opt);

            // Save the compressed PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPdf}'.");
    }
}