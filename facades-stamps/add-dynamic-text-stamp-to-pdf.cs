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
        const string author     = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Create a text stamp with interpolated content (date and author)
            string stampText = $"Created on {DateTime.Now:yyyy-MM-dd} by {author}";
            Aspose.Pdf.TextStamp textStamp = new Aspose.Pdf.TextStamp(stampText);

            // Configure stamp appearance (optional)
            textStamp.Background = false;                     // draw on top of page content
            textStamp.Opacity    = 0.7;                       // semi‑transparent
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            textStamp.YIndent = 20;                           // distance from bottom edge

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp added and saved to '{outputPath}'.");
    }
}