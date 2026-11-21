using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_endnote.pdf";

        const string endnoteText = "This is an endnote with **bold** and *italic* formatting.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Get the last page (1‑based indexing)
                Page lastPage = doc.Pages[doc.Pages.Count];

                // Position near the bottom‑left corner
                double marginLeft = 50;   // points from the left edge
                double marginBottom = 50; // points from the bottom edge

                // Create the TextFragment and set its position
                TextFragment fragment = new TextFragment(endnoteText)
                {
                    Position = new Aspose.Pdf.Text.Position(marginLeft, marginBottom)
                };

                // Modify the existing TextState (property is read‑only)
                fragment.TextState.Font = FontRepository.FindFont("Arial");
                fragment.TextState.FontSize = 12;
                fragment.TextState.FontStyle = FontStyles.Bold | FontStyles.Italic;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the fragment to the page
                lastPage.Paragraphs.Add(fragment);

                // Save the updated document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Endnote added and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
