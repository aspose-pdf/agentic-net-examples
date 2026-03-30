using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "portfolio.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Extract each image to a separate PDF file
        List<string> extractedPdfFiles = new List<string>();
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        extractor.ExtractImage();
        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string imagePdf = "image-" + imageIndex + ".pdf";
            extractor.GetNextImage(imagePdf);
            extractedPdfFiles.Add(imagePdf);
            imageIndex++;
        }

        // Create a new PDF and add each extracted image PDF as a page
        using (Document portfolio = new Document())
        {
            foreach (string imgPdfPath in extractedPdfFiles)
            {
                using (Document imgDoc = new Document(imgPdfPath))
                {
                    portfolio.Pages.Add(imgDoc.Pages);
                }
            }
            portfolio.Save(outputPdf);
        }

        // Optional cleanup of temporary files
        foreach (string tempFile in extractedPdfFiles)
        {
            try
            {
                File.Delete(tempFile);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Could not delete temporary file '{tempFile}': {ex.Message}");
            }
        }

        Console.WriteLine($"Portfolio PDF created: {outputPdf}");
    }
}