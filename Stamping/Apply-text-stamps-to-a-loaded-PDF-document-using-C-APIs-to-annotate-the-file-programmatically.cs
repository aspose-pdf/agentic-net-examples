using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampText  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired content
            TextStamp textStamp = new TextStamp(stampText);

            // Configure visual appearance of the stamp
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.FontSize = 48;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            textStamp.Opacity = 0.5; // semi‑transparent
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Center;
            textStamp.Background = false; // draw on top of page content

            // Apply the stamp to every page (pages are 1‑based)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the annotated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}