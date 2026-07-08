using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // required for font substitution

class PdfToPngConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PngPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // Font substitution: replace any missing font with Arial.
        // Aspose.Pdf does not expose FontSubstitutionEnabled on Document,
        // so we use the static FontRepository.Substitutions collection.
        // The wildcard "*" substitutes every missing font with the
        // specified fallback (Arial). Adjust the source name if you
        // need a more specific mapping.
        // ------------------------------------------------------------
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        // Load the PDF document (substitution rules are already in place)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Use PdfConverter (Facade) to convert each page to PNG
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the already‑loaded document
                converter.BindPdf(pdfDocument);

                // Set the page range to convert (all pages)
                converter.StartPage = 1;
                converter.EndPage   = pdfDocument.Pages.Count; // total pages in the document

                int pageNumber = converter.StartPage;

                // Convert pages one by one while images are available
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                    // Save the current page as PNG
                    converter.GetNextImage(outputPath, ImageFormat.Png);

                    pageNumber++;
                }
            }
        }

        Console.WriteLine($"Conversion completed. PNG files are saved in '{outputFolder}'.");
    }
}
