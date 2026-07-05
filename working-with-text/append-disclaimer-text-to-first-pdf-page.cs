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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Ensure new paragraphs are added after the existing ones
            firstPage.IsAddParagraphsAfterLast = true;

            // Create a TextFragment with the disclaimer text
            TextFragment disclaimer = new TextFragment("Disclaimer: This document is confidential.");

            // Optional styling
            disclaimer.TextState.FontSize = 10;
            disclaimer.TextState.Font = FontRepository.FindFont("Helvetica");
            disclaimer.TextState.ForegroundColor = Color.Gray;

            // Append the TextFragment to the end of the page's paragraph collection
            firstPage.Paragraphs.Add(disclaimer);

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with disclaimer to '{outputPath}'.");
    }
}