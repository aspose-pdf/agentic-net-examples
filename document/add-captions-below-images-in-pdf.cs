using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class AddCaptionsToImages
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
            int imageCounter = 1;

            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a styled caption text
                    TextState captionState = new TextState
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 10,
                        ForegroundColor = Aspose.Pdf.Color.Gray
                    };

                    // Build the caption paragraph
                    TextParagraph captionParagraph = new TextParagraph
                    {
                        // Position the caption below the image.
                        // Here we use a simple heuristic: place it 20 points below the image's bottom.
                        // Since XImage does not expose its exact location, we use a fixed position.
                        Rectangle = new Aspose.Pdf.Rectangle(50, 50, 550, 70),
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    captionParagraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;
                    captionParagraph.AppendLine($"Figure {imageCounter}: Image description", captionState);

                    // Append the caption to the page
                    TextBuilder builder = new TextBuilder(page);
                    builder.AppendParagraph(captionParagraph);

                    imageCounter++;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Captions added. Saved to '{outputPath}'.");
    }
}