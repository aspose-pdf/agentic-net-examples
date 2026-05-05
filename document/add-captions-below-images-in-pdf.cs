using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_captions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            int imageIndex = 1; // Counter for generating unique captions

            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Simple positioning: place each caption a fixed distance from the bottom,
                    // offsetting each subsequent caption to avoid overlap.
                    float captionX = 50;                     // left margin
                    float captionY = 20 * imageIndex;        // vertical position
                    float captionWidth = 200;
                    float captionHeight = 15;

                    // Create a styled text fragment for the caption
                    TextFragment captionFragment = new TextFragment($"Image {imageIndex} caption");
                    captionFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                    captionFragment.TextState.FontSize = 10;
                    captionFragment.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;

                    // Build a paragraph and set its rectangle and formatting
                    TextParagraph paragraph = new TextParagraph
                    {
                        Rectangle = new Aspose.Pdf.Rectangle(captionX, captionY, captionX + captionWidth, captionY + captionHeight),
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;
                    paragraph.AppendLine(captionFragment);

                    // Append the paragraph to the current page
                    TextBuilder builder = new TextBuilder(page);
                    builder.AppendParagraph(paragraph);

                    imageIndex++;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with captions saved to '{outputPath}'.");
    }
}