using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Transparent Text");
            tf.Position = new Position(100, 600); // place the text on the page

            // Configure the text state (font, size, color)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 24;
            // Set 50% opacity by using an ARGB color (A = 128 out of 255)
            tf.TextState.ForegroundColor = Color.FromArgb(128, 0, 0, 255); // semi‑transparent blue

            // Append the configured text fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
