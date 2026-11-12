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

        // Create a text stamp that will be placed behind existing content
        TextStamp textStamp = new TextStamp(watermarkText)
        {
            Background = true,               // render behind page content
            Opacity = 0.2,                   // semi‑transparent
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            TextState =
            {
                FontSize = 72,
                FontStyle = FontStyles.Bold,
                Font = FontRepository.FindFont("Arial"),
                ForegroundColor = Color.Gray
            }
        };

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply the stamp to pages 2‑5 (or up to the last page)
            int lastPage = Math.Min(5, doc.Pages.Count);
            for (int pageNum = 2; pageNum <= lastPage; pageNum++)
            {
                // No need to set a Rectangle – alignment properties cover the whole page
                doc.Pages[pageNum].AddStamp(textStamp);
            }

            // Save the watermarked PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
