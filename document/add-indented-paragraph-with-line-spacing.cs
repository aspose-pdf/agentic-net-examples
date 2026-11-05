using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_indented.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page
                if (doc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The document contains no pages.");
                    return;
                }

                // Choose the first page (pages are 1‑based)
                Page page = doc.Pages[1];

                // Create a text fragment with the desired content
                TextFragment fragment = new TextFragment("Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                                                         "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");

                // ----- Paragraph indentation -----
                // Use the MarginInfo to set a left indent (e.g., 30 points)
                fragment.Margin = new MarginInfo { Left = 30 };

                // ----- Line spacing -----
                // Set the line spacing factor (e.g., 1.5 for 150% line height)
                fragment.TextState.LineSpacing = 1.5f;

                // Optional: set other text appearance properties
                fragment.TextState.FontSize = 12;
                fragment.TextState.Font = FontRepository.FindFont("Arial");
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the fragment to the page's paragraphs collection
                page.Paragraphs.Add(fragment);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with indentation and line spacing to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}