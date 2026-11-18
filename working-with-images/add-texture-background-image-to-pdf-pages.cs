using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string backgroundImagePath = "background.png";

        // Verify input files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Add the background image to each page
                foreach (Page page in doc.Pages)
                {
                    // Create an ImageStamp for the background texture
                    ImageStamp bgStamp = new ImageStamp(backgroundImagePath)
                    {
                        // Place the stamp behind existing page content
                        Background = true,
                        // Make the texture semi‑transparent for a subtle effect
                        Opacity = 0.5,
                        // Resize the stamp to cover the whole page
                        Width = page.PageInfo.Width,
                        Height = page.PageInfo.Height,
                        // Ensure the stamp starts at the page origin
                        XIndent = 0,
                        YIndent = 0
                    };

                    // Apply the stamp to the current page
                    page.AddStamp(bgStamp);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Background image added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
