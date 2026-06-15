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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Ensure new content is added after the existing paragraphs
            page.IsAddParagraphsAfterLast = true;

            // Create a TextFragment containing the disclaimer
            TextFragment disclaimerFragment = new TextFragment(disclaimer);
            disclaimerFragment.TextState.FontSize = 9;
            disclaimerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            disclaimerFragment.TextState.ForegroundColor = Color.Gray;

            // Append the TextFragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(disclaimerFragment);

            // Save the modified document (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Disclaimer appended and saved to '{outputPath}'.");
    }
}