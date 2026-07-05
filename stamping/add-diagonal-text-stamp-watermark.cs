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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be used on every page
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Rotate the stamp by 45 degrees (arbitrary angle)
                RotateAngle = 45,

                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Make the stamp semi‑transparent
                Opacity = 0.3,

                // Optional: adjust size to cover the page diagonally
                // Width and Height can be left default; the stamp will be scaled automatically
            };

            // Configure the visual appearance of the text
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 72;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            // Apply the stamp to each page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}