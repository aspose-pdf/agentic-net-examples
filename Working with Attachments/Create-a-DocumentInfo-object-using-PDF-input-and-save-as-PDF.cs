using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Access the existing DocumentInfo object and modify its properties directly
            pdfDoc.Info.Title = "Processed PDF";
            pdfDoc.Info.Author = "Aspose.Pdf Example";

            // Save the document as PDF (extension determines format)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}