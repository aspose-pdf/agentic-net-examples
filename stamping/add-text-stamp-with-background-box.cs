using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for TextStamp (inherits from Stamp)
using Aspose.Pdf.Text;       // for FontRepository and TextState
using Aspose.Pdf.Drawing;    // for Aspose.Pdf.Rectangle if needed

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page and add a TextStamp with a background box
            foreach (Page page in doc.Pages)
            {
                // Create a TextStamp with the desired text
                TextStamp textStamp = new TextStamp("CONFIDENTIAL");

                // Position the stamp (centered on the page)
                textStamp.HorizontalAlignment = HorizontalAlignment.Center;
                textStamp.VerticalAlignment   = VerticalAlignment.Center;

                // Make the stamp appear behind page content (background)
                textStamp.Background = true;

                // Slightly transparent to keep page content visible
                textStamp.Opacity = 0.6;

                // Configure the visual appearance of the text via the read‑only TextState object
                textStamp.TextState.FontSize        = 36;
                textStamp.TextState.Font            = FontRepository.FindFont("Arial");
                textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.White;
                textStamp.TextState.BackgroundColor = Aspose.Pdf.Color.Black; // background box color

                // Add the stamp to the current page
                page.AddStamp(textStamp);
            }

            // Save the modified PDF (inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}
