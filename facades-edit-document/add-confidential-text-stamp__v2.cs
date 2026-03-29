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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired value
            TextStamp stamp = new TextStamp("Confidential");

            // Set the stamp text color to red
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Make the stamp semi‑transparent (50% opacity)
            stamp.Opacity = 0.5f;

            // Place the stamp behind page content (optional)
            stamp.Background = true;

            // Center the stamp on each page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}