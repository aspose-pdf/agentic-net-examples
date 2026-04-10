using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "sample.pdf";
        const string outputTxt = "sample_text.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfExtractor facade
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF document to the extractor
        extractor.BindPdf(inputPdf);

        // Do NOT invoke extractor.ExtractImage(); – this prevents any image extraction
        // (If called, images would be extracted according to ExtractImageMode.)
        // Optionally, you can set the mode, but it has no effect when ExtractImage() is not called.
        // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

        // Extract only the textual content
        extractor.ExtractText();

        // Save the extracted text to a file
        extractor.GetText(outputTxt);

        // Release resources held by the extractor
        extractor.Close();

        Console.WriteLine($"Text extracted to '{outputTxt}'. Images were ignored.");
    }
}