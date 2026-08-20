using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextStamp
using Aspose.Pdf.Annotations; // not needed but harmless

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Dynamic values to embed in the stamp
        string author = "John Doe";
        string date   = DateTime.Now.ToString("yyyy-MM-dd");

        // Create the stamp text using string interpolation
        string stampText = $"Author: {author} | Date: {date}";

        // Process the PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the interpolated text
            TextStamp textStamp = new TextStamp(stampText)
            {
                // Example visual settings (optional)
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                Opacity             = 0.5f,
                // Adjust the stamp to fit the page width if needed
                AutoAdjustFontSizeToFitStampRectangle = true,
                // Position can be fine‑tuned via margins
                RightMargin = 20,
                BottomMargin = 20
            };

            // Apply the stamp to each page (or a specific page)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}