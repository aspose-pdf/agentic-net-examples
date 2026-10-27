using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Starting number for Bates numbering
                int batesNumber = 1000;

                // Add a TextStamp to each page with the current Bates number and a dash separator
                foreach (Page page in doc.Pages)
                {
                    // Create the stamp with the current number
                    TextStamp stamp = new TextStamp($"{batesNumber}-");

                    // Positioning
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;

                    // Configure the appearance via the read‑only TextState object
                    stamp.TextState.FontSize = 12;
                    stamp.TextState.ForegroundColor = Color.Black;

                    // Add the stamp to the page
                    page.AddStamp(stamp);

                    batesNumber++; // increment for next page
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bates numbering added. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
