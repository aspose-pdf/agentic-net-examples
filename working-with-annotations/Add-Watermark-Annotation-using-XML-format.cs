using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string xfdfFile   = "watermark.xfdf"; // XFDF (XML) containing WatermarkAnnotation
        const string outputPdf  = "output.pdf";     // result PDF with watermark

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(xfdfFile))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfFile}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Import annotations (including WatermarkAnnotation) from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfFile);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermark annotation added. Saved to '{outputPdf}'.");
    }
}