using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_text.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment textFragment = new TextFragment("Rotated Text");

            // Set the absolute position of the fragment on the page
            textFragment.Position = new Position(200, 500);

            // Configure visual appearance
            textFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            textFragment.TextState.FontSize = 24;

            // Rotate the text by specifying the angle in degrees
            // The Rotation property is part of TextFragmentState (exposed via TextState)
            textFragment.TextState.Rotation = 45; // 45° clockwise rotation

            // Use TextBuilder (core API) to render the fragment onto the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(textFragment);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rotated text saved to '{outputPath}'.");
    }
}