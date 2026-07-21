using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "superscript.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Add normal text "E = mc"
            TextFragment normal = new TextFragment("E = mc");
            normal.TextState.FontSize = 12;               // regular font size
            page.Paragraphs.Add(normal);

            // Insert a superscript "2" by raising the text baseline
            // Set a positive rise (e.g., 5 points) to move the text upward
            page.Contents.Add(new SetTextRise(5));
            // Show the superscript character
            page.Contents.Add(new ShowText("2"));
            // Reset the rise back to baseline for subsequent text
            page.Contents.Add(new SetTextRise(0));

            // Save the document (no explicit SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Superscript PDF saved to '{outputPath}'.");
    }
}