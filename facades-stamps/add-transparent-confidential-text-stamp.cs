using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, Font, Color, TextStamp, HorizontalAlignment, VerticalAlignment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "confidential_output.pdf";

        // Pages on which the stamp should appear (1‑based indexing)
        int[] pagesToStamp = new int[] { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;

        // Configure the text stamp
        TextStamp stamp = new TextStamp("CONFIDENTIAL")
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center,
            RotateAngle = 0f,               // no rotation
            Opacity = 0.7f                  // 70 % opacity
        };
        stamp.TextState.Font = FontRepository.FindFont("Arial");
        stamp.TextState.FontSize = 72;
        stamp.TextState.ForegroundColor = Color.Red; // use ForegroundColor, not FontColor
        stamp.TextState.BackgroundColor = Color.Transparent;

        // Apply the stamp to each valid page number using Document.Pages[i].AddStamp
        foreach (int pageNumber in pagesToStamp)
        {
            if (pageNumber < 1 || pageNumber > pageCount)
                continue; // skip invalid page numbers

            doc.Pages[pageNumber].AddStamp(stamp);
        }

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Confidential stamp applied. Output saved to '{outputPath}'.");
    }
}
