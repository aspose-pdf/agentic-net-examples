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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Ensure new content is added after existing paragraphs
            page.IsAddParagraphsAfterLast = true;

            // Create the disclaimer text fragment
            TextFragment disclaimer = new TextFragment("Disclaimer: This document is confidential.");
            disclaimer.TextState.FontSize = 10;
            disclaimer.TextState.Font = FontRepository.FindFont("Helvetica");
            disclaimer.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            // Append the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(disclaimer);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}