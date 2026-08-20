using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Create a sample input PDF because the sandbox does not contain any files.
        // ---------------------------------------------------------------------
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Create a minimal PDF with a single blank page.
        using (Document seed = new Document())
        {
            seed.Pages.Add();
            seed.Save(inputPdfPath);
        }

        // ---------------------------------------------------------------
        // Load a TrueType font. Instead of reading an external .ttf file we
        // obtain a built‑in font (e.g., Arial) from the Aspose.Pdf font cache.
        // ---------------------------------------------------------------
        Font customFont = FontRepository.FindFont("Arial");
        // Ensure the font will be embedded in the resulting PDF.
        customFont.IsEmbedded = true;

        // ---------------------------------------------------------------
        // Open the source PDF, add text using the custom font, and save.
        // ---------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a text fragment with the desired content.
            TextFragment fragment = new TextFragment("Hello with custom font!");

            // Assign the custom font to the fragment's TextState.
            fragment.TextState.Font = customFont;
            fragment.TextState.FontSize = 16; // optional size adjustment

            // Add the fragment to the first page.
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}' with custom font applied.");
    }
}
