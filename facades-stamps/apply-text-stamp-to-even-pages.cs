using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "evenStamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        Document doc = new Document(inputPath);

        // Prepare a text stamp that will be reused for every even page
        TextStamp stamp = new TextStamp("Even Page");
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 36;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray; // fully‑qualified color
        stamp.HorizontalAlignment = HorizontalAlignment.Left;
        stamp.VerticalAlignment = VerticalAlignment.Bottom;
        stamp.Background = true;          // place behind existing content (use Background, not IsBackground)
        stamp.Opacity = 0.5f;                // semi‑transparent

        // Apply the stamp only to even‑numbered pages (1‑based indexing)
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            if (i % 2 == 0) // even page
            {
                doc.Pages[i].AddStamp(stamp);
            }
        }

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}