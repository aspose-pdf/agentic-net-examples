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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Ensure new paragraphs are added after the existing ones
            page.IsAddParagraphsAfterLast = true;

            // Create a TextFragment containing the disclaimer
            TextFragment disclaimerFragment = new TextFragment(disclaimer);
            disclaimerFragment.TextState.FontSize = 10;
            disclaimerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            disclaimerFragment.TextState.ForegroundColor = Color.Gray;

            // Append the TextFragment to the end of the page's paragraph collection
            page.Paragraphs.Add(disclaimerFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}