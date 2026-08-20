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

        // Load the PDF, set standard and custom properties, then save as PPTX
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Standard property
            pdfDoc.Info.Subject = "Presentation Subject";

            // Custom property – use the DocumentInfo indexer (CustomProperties collection does not exist)
            pdfDoc.Info["Company"] = "Acme Corporation";

            // PPTX save options (available directly under Aspose.Pdf namespace)
            var pptxOptions = new PptxSaveOptions();

            // Convert and save as PPTX
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}