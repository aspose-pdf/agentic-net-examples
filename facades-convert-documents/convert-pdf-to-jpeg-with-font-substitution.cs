using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat for JPEG output
using Aspose.Pdf;                           // Core PDF API
using Aspose.Pdf.Facades;                   // PdfConverter facade
using Aspose.Pdf.Text;                      // PdfSaveOptions (for font substitution)

class Program
{
    static void Main()
    {
        // Input PDF file (will be created if it does not exist)
        const string inputPdfPath = "input.pdf";

        // Folder where JPEG images will be saved
        const string outputFolder = "Images";

        // Font to use when the original PDF references a missing font
        const string substituteFontName = "Arial";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // ---------------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal one if it is missing.
        // This makes the sample self‑contained and prevents the FileNotFoundException.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document tempDoc = new Document())
            {
                // Add a simple page with some text so that the conversion has content.
                Page page = tempDoc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF generated because 'input.pdf' was missing."));
                tempDoc.Save(inputPdfPath);
            }
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure font substitution: any missing font will be replaced by the specified font
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                DefaultFontName = substituteFontName
            };

            // Save the (potentially font‑substituted) PDF into a memory stream
            using (MemoryStream pdfStream = new MemoryStream())
            {
                pdfDoc.Save(pdfStream, saveOptions);
                pdfStream.Position = 0; // Reset stream position for reading

                // Convert each page to a JPEG image using PdfConverter
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the in‑memory PDF to the converter
                    converter.BindPdf(pdfStream);

                    // Prepare the converter (parses the document, calculates page count, etc.)
                    converter.DoConvert();

                    int pageNumber = 1;
                    // -----------------------------------------------------------------
                    // Suppress the platform‑specific CA1416 warning for ImageFormat.Jpeg.
                    // The Aspose API internally handles the platform differences.
                    // -----------------------------------------------------------------
#pragma warning disable CA1416 // Validate platform compatibility
                    while (converter.HasNextImage())
                    {
                        string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                        // Save the current page as JPEG with a quality setting (0‑100)
                        converter.GetNextImage(outputPath, ImageFormat.Jpeg, 90);

                        pageNumber++;
                    }
#pragma warning restore CA1416 // Validate platform compatibility
                }
            }
        }

        Console.WriteLine("PDF conversion to JPEG images completed.");
    }
}
