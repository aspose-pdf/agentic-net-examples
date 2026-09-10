using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header.pdf";
        const string headerText = "My Document Header";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Loop through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text stamp that will act as a header
                TextStamp headerStamp = new TextStamp(headerText)
                {
                    // Position the stamp at the top centre of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,
                    TopMargin           = 10 // distance from the top edge
                };

                // Configure font and appearance via the existing TextState instance
                headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                headerStamp.TextState.FontSize = 12;
                headerStamp.TextState.FontStyle = FontStyles.Regular;
                headerStamp.TextState.ForegroundColor = Color.Black;

                // Add the stamp to the current page
                page.AddStamp(headerStamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with header to '{outputPath}'.");
    }
}
