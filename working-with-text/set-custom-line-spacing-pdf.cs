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
        const float leading = 15f; // Desired line spacing

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPath))
            {
                // Create a text fragment with multiple lines
                TextFragment fragment = new TextFragment("First line\nSecond line\nThird line");

                // Set line spacing via TextState.LineSpacing (the correct property)
                fragment.TextState.LineSpacing = leading;

                // Optional: set other text properties
                fragment.TextState.FontSize = 12;
                fragment.TextState.Font = FontRepository.FindFont("Arial");

                // Add the fragment to the first page (1‑based indexing)
                Page page = doc.Pages[1];
                page.Paragraphs.Add(fragment);

                // Save the modified PDF (lifecycle rule: save inside using)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
