using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, add custom properties, and convert to PPTX
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Add a custom property for the company name using the DocumentInfo indexer
            pdfDoc.Info["Company"] = "Acme Corp";

            // Set the standard Subject property
            pdfDoc.Info.Subject = "Quarterly Report";

            // Initialize PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the document as PPTX using the save options
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF converted to PPTX with custom properties saved at '{outputPptxPath}'.");
    }
}
