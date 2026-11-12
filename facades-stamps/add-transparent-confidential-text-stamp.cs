using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Pages to which the stamp will be applied (1‑based indexing)
        int[] pagesToStamp = { 1, 2, 3 }; // adjust as needed

        // Load the source PDF document
        Document pdfDocument = new Document(inputPath);

        // Configure the text stamp
        TextStamp textStamp = new TextStamp(stampText)
        {
            TextState =
            {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 48,
                ForegroundColor = Color.FromRgb(1, 0, 0) // red color
            },
            Opacity = 0.7, // 70% opacity
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // Apply the stamp to each selected page
        foreach (int pageNumber in pagesToStamp)
        {
            // Validate page number against the document page count
            if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
                continue;

            pdfDocument.Pages[pageNumber].AddStamp(textStamp);
        }

        // Save the stamped PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
