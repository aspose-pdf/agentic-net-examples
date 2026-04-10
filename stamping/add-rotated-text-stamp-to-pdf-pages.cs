using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text stamp
                TextStamp stamp = new TextStamp("STAMP")
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Opacity = 0.5f,
                    Background = false
                };

                // Rotate the stamp 90° to match portrait‑to‑landscape orientation
                stamp.RotateAngle = 90f;

                // Set visual style via the existing TextState object
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 48;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

                // Apply the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}