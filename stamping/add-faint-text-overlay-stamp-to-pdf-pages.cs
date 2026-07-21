using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, process, and save inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be applied to every page
            TextStamp stamp = new TextStamp("Sample Overlay")
            {
                // Set the stamp to be drawn on top of page content
                Background = false,
                // Center the stamp horizontally and vertically
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Make the stamp faint
                Opacity = 0.4
            };

            // Configure visual appearance of the text
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            // Apply the same stamp to each page (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(stamp);
            }

            // Save the modified PDF (create, load and save rules are respected)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}