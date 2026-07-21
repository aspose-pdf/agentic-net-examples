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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for proper disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Set standard document property
                pdfDoc.Info.Subject = "Presentation generated from PDF";

                // Set a custom property using the DocumentInfo indexer
                pdfDoc.Info["Company"] = "Acme Corporation";

                // Initialize PPTX save options (required for non‑PDF output)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as PPTX using the explicit save options
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
