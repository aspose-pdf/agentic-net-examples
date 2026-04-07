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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Multi‑line disclaimer text (use newline characters)
            string disclaimer = "Disclaimer:\nThis document is confidential.\nDo not distribute.";

            // Create a TextStamp with the disclaimer
            TextStamp stamp = new TextStamp(disclaimer);

            // Customize text appearance
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 12;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            // Set custom line spacing (points)
            stamp.TextState.LineSpacing = 5f;

            // Position the stamp (centered at the bottom of each page)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;
            stamp.BottomMargin        = 20;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}