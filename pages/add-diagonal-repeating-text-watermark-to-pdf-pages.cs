using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply the watermark to each page
            foreach (Page page in doc.Pages)
            {
                // Create a text stamp with the desired watermark text
                TextStamp stamp = new TextStamp(watermarkText);

                // Configure text appearance
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 72;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Make the stamp appear behind page content
                stamp.Background = true;

                // Set opacity (0.0 = fully transparent, 1.0 = fully opaque)
                stamp.Opacity = 0.5f;

                // Rotate the stamp to create a diagonal effect
                stamp.RotateAngle = -45;

                // Size the stamp to cover the entire page so the text repeats
                stamp.Width = page.PageInfo.Width;
                stamp.Height = page.PageInfo.Height;

                // Position the stamp at the page origin
                stamp.XIndent = 0;
                stamp.YIndent = 0;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}