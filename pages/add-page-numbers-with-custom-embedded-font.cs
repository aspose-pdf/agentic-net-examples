using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string ttfFontPath = "customfont.ttf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(ttfFontPath))
        {
            Console.Error.WriteLine($"TTF font file not found: {ttfFontPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Open the external TrueType font and ensure it will be embedded.
            Font customFont = FontRepository.OpenFont(ttfFontPath);
            customFont.IsEmbedded = true; // embed the font into the PDF

            // Define margins for page number placement.
            const double marginRight = 50; // points from the right edge
            const double marginBottom = 30; // points from the bottom edge
            const float fontSize = 12f; // FontSize expects a float – use a float literal

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create the page number text.
                string pageNumberText = i.ToString();

                TextFragment tf = new TextFragment(pageNumberText);
                tf.TextState.Font = customFont;
                tf.TextState.FontSize = fontSize; // explicit float matches the property type
                tf.TextState.ForegroundColor = Color.Black; // cross‑platform color

                // Position the text at the bottom‑right corner.
                double x = page.PageInfo.Width - marginRight;
                double y = marginBottom;
                // Position constructor expects double values – cast explicitly to float if needed.
                tf.Position = new Position((float)x, (float)y);

                // Add the text fragment to the page.
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPdfPath}'.");
    }
}
