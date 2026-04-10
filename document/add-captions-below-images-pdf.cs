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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            int captionNumber = 1;

            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Prepare caption text
                    string captionText = $"Figure {captionNumber}";

                    // Create a styled TextParagraph
                    TextParagraph paragraph = new TextParagraph();

                    // Define the rectangle where the caption will be placed.
                    // Here we position it near the bottom of the page, offset by the caption number.
                    // PageInfo.Width/Height are double, so cast to float for the Rectangle constructor.
                    float pageWidth  = (float)page.PageInfo.Width;
                    float pageHeight = (float)page.PageInfo.Height;
                    float left   = 50f;
                    float right  = pageWidth - 50f;
                    float top    = pageHeight - 50f - (captionNumber * 20f);
                    float bottom = top - 20f; // height of the paragraph

                    paragraph.Rectangle = new Aspose.Pdf.Rectangle(left, bottom, right, top);

                    // Enable word‑wrap
                    paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

                    // Define text styling
                    TextState textState = new TextState
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 12,
                        ForegroundColor = Aspose.Pdf.Color.DarkGray
                    };

                    // Add the caption line with the defined style
                    paragraph.AppendLine(captionText, textState);

                    // Append the paragraph to the page using TextBuilder
                    TextBuilder builder = new TextBuilder(page);
                    builder.AppendParagraph(paragraph);

                    captionNumber++;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with captions saved to '{outputPath}'.");
    }
}
