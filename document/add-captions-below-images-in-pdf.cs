using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_captions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                int imageIndex = 1; // Counter for captions

                // Iterate over all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Iterate over each image resource on the page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Create a caption text fragment
                        string captionText = $"Figure {imageIndex}: Image description";
                        TextFragment caption = new TextFragment(captionText);

                        // Style the caption (e.g., italic, gray, smaller font)
                        caption.TextState.FontSize = 10;
                        caption.TextState.FontStyle = FontStyles.Italic;
                        caption.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                        // Position the caption.
                        // Here we place it at a fixed offset from the bottom left of the page.
                        // Adjust Y coordinate as needed for your layout.
                        caption.Position = new Position(50, 50 + (imageIndex - 1) * 15);

                        // Add the caption to the page's paragraphs collection
                        page.Paragraphs.Add(caption);

                        imageIndex++;
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Captions added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}