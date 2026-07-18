using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
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
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text stamp with the desired watermark text
                TextStamp stamp = new TextStamp(watermarkText);

                // Make the stamp a background element so it appears behind page content
                stamp.Background = true;

                // Set opacity (0.0 – 1.0)
                stamp.Opacity = 0.3f;

                // Rotate the stamp to create a diagonal effect
                stamp.RotateAngle = -45; // degrees

                // Size the stamp to cover the whole page
                stamp.Width  = page.PageInfo.Width;
                stamp.Height = page.PageInfo.Height;

                // Center the text within the stamp rectangle
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;
                // The TextAlignment property is not available in older SDK versions;
                // horizontal/vertical alignment already centers the text.

                // Configure text appearance
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 72;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                stamp.TextState.FontStyle = FontStyles.Bold;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
