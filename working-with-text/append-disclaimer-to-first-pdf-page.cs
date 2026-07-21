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
        const string disclaimer = "Disclaimer: This document is confidential.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Ensure new paragraphs are added after the existing content
            firstPage.IsAddParagraphsAfterLast = true;

            // Create a TextFragment with the disclaimer text
            TextFragment disclaimerFragment = new TextFragment(disclaimer);
            // Optional styling
            disclaimerFragment.TextState.FontSize = 9;
            disclaimerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            disclaimerFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            // Append the TextFragment to the end of the page's paragraph collection
            firstPage.Paragraphs.Add(disclaimerFragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with disclaimer to '{outputPath}'.");
    }
}