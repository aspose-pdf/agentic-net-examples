using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextStamp and TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a text stamp that will be applied to every page.
        TextStamp stamp = new TextStamp("CONFIDENTIAL")
        {
            // Position the stamp in the centre of each page.
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center,
            // Make the stamp semi‑transparent and place it behind page content.
            Opacity   = 0.5f,
            Background = true
        };
        // Configure the visual appearance via the read‑only TextState object.
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 48;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

        // Load the PDF, apply the stamp to each page, and save.
        using (Document doc = new Document(inputPath))
        {
            // Document.AddStamp does not exist; apply the stamp per page.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
