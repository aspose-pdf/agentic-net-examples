using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Text; // required for TextBuilder, TextParagraph, FontRepository

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page
            foreach (Page page in doc.Pages)
            {
                // Iterate through each image resource on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a simple caption text (you can customize per image if needed)
                    string captionText = "Figure: Image description";

                    // Define a rectangle for the caption.
                    // Position it 10 points below the bottom of the page (adjust as needed).
                    // Using fully qualified types to avoid ambiguity.
                    Aspose.Pdf.Rectangle captionRect = new Aspose.Pdf.Rectangle(
                        50,                                 // left
                        20,                                 // bottom (10 points above page bottom)
                        page.PageInfo.Width - 50,           // right
                        40);                                // top

                    // Create a TextParagraph and style it
                    TextParagraph paragraph = new TextParagraph
                    {
                        Rectangle = captionRect,
                        // Enable word wrap
                        FormattingOptions = { WrapMode = TextFormattingOptions.WordWrapMode.ByWords },
                        // Center align the caption
                        HorizontalAlignment = HorizontalAlignment.Center
                    };

                    // Set text style via TextState
                    TextState ts = new TextState
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 12,
                        ForegroundColor = Aspose.Pdf.Color.Gray
                    };

                    // Append the caption line with the defined style
                    paragraph.AppendLine(captionText, ts);

                    // Append the paragraph to the page using TextBuilder
                    TextBuilder builder = new TextBuilder(page);
                    builder.AppendParagraph(paragraph);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with captions saved to '{outputPath}'.");
    }
}