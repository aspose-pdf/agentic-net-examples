using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_captions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imageIndex = 0;

                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;

                    // Create a styled text paragraph to serve as the caption
                    TextParagraph caption = new TextParagraph();

                    // Position the caption near the bottom left of the page.
                    // Adjust the rectangle as needed for your layout.
                    caption.Rectangle = new Aspose.Pdf.Rectangle(50, 50, 250, 70);

                    // Enable word wrapping
                    caption.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

                    // Center the caption horizontally within the rectangle
                    caption.HorizontalAlignment = HorizontalAlignment.Center;

                    // Add the caption text (you can customize the text as required)
                    string captionText = $"Image {imageIndex} on page {pageNum}";
                    caption.AppendLine(captionText);

                    // Append the paragraph to the page using TextBuilder
                    TextBuilder builder = new TextBuilder(page);
                    builder.AppendParagraph(caption);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with captions saved to '{outputPath}'.");
    }
}