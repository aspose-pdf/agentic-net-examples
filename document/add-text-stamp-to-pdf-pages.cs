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

        // Load the PDF, add a text stamp to each page, and save.
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                // Create a text stamp with the desired message.
                TextStamp stamp = new TextStamp(stampText);

                // Position the stamp in the center of the page.
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Make the stamp semi‑transparent.
                stamp.Opacity = 0.5f;

                // Define visual style (font, size, color).
                TextState ts = stamp.TextState;
                ts.Font = FontRepository.FindFont("Helvetica");
                ts.FontSize = 48;
                ts.ForegroundColor = Aspose.Pdf.Color.Red;

                // Add the stamp to the current page.
                page.AddStamp(stamp);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}