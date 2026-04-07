using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Transparent Text");

            // Set the position of the fragment on the page
            fragment.Position = new Position(100, 700);

            // Configure opacity via the ForegroundColor's alpha channel (0‑255).
            // 128 = 50% opacity, color is black (0,0,0).
            fragment.TextState.ForegroundColor = Color.FromArgb(128, 0, 0, 0);

            // Append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
