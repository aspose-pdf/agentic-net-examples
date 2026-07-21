using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Verify that page 10 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The document contains fewer than 10 pages.");
                return;
            }

            // Create a TextStamp with the desired text
            Aspose.Pdf.TextStamp textStamp = new Aspose.Pdf.TextStamp(stampText);

            // Configure the visual appearance of the stamp
            Aspose.Pdf.Text.TextState ts = textStamp.TextState;
            ts.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            ts.FontSize = 24;                         // Example size
            ts.Underline = true;                      // Underline decoration
            ts.BackgroundColor = Aspose.Pdf.Color.Yellow; // Yellow background
            ts.ForegroundColor = Aspose.Pdf.Color.Black; // Text color

            // Center the stamp horizontally and vertically on the page
            textStamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = Aspose.Pdf.VerticalAlignment.Center;

            // Add the stamp to page 10
            Aspose.Pdf.Page page = doc.Pages[10];
            page.AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}