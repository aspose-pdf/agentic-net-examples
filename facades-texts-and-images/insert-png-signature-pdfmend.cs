using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string imagePath = "signature.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine("Signature image not found: " + imagePath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            int[] pageNumbers = new int[pageCount];
            int i = 0;
            while (i < pageCount)
            {
                pageNumbers[i] = i + 1; // 1‑based page indexing
                i++;
            }

            using (PdfFileMend mend = new PdfFileMend())
            {
                mend.BindPdf(doc);
                // Bottom‑left corner, 100 × 50 points size
                float lowerLeftX = 0f;
                float lowerLeftY = 0f;
                float upperRightX = 100f;
                float upperRightY = 50f;

                bool added = mend.AddImage(imagePath, pageNumbers, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
                if (!added)
                {
                    Console.Error.WriteLine("Failed to add signature image to PDF.");
                }
                mend.Save(outputPath);
            }
        }

        Console.WriteLine("Signature added to all pages. Output saved as " + outputPath);
    }
}