using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            int pageNumber = 1;

            // Iterate through each page in the document
            foreach (Page page in doc.Pages)
            {
                int imageIndex = 0;

                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a simple caption text for the image
                    string captionText = $"Image {imageIndex + 1} on page {pageNumber}";

                    // Create a TextFragment and style it
                    TextFragment captionFragment = new TextFragment(captionText);
                    captionFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                    captionFragment.TextState.FontSize = 10;
                    captionFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                    // Position the caption below the previous one (stacked vertically)
                    // Adjust X/Y as needed; here we start at (50,50) and offset each caption by 15 points
                    double posX = 50;
                    double posY = 50 + imageIndex * 15;
                    captionFragment.Position = new Position(posX, posY);

                    // Append the caption to the page using TextBuilder
                    TextBuilder builder = new TextBuilder(page);
                    builder.AppendText(captionFragment);

                    imageIndex++;
                }

                pageNumber++;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Captions added and saved to '{outputPath}'.");
    }
}