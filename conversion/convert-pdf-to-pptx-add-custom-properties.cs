using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set standard document properties
            pdfDoc.Info.Subject = "Presentation generated from PDF";

            // Add custom document properties (e.g., Company) using the indexer
            pdfDoc.Info["Company"] = "Acme Corporation";

            // Prepare PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the document as PPTX using the save options
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
    }
}
