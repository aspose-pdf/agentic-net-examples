using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfToJpegConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Directory where JPEG images will be saved
        const string outputDirectory = "Images";
        // Font to use when the original PDF references missing fonts
        const string substituteFontName = "Arial";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // ---------------------------------------------------------------------
        // Create a placeholder PDF if the expected input file does not exist.
        // This makes the example self‑contained and prevents a FileNotFoundException
        // in the sandbox environment.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdfPath);
        }

        // Load the source PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure font substitution options
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                // Use the specified font for any missing fonts in the source PDF
                DefaultFontName = substituteFontName,
                // Font embedding is handled automatically; the FontEmbeddingMode property
                // is not available in the current Aspose.PDF version and therefore omitted.
            };

            // Save the PDF with the substitution applied into a memory stream
            using (MemoryStream pdfStream = new MemoryStream())
            {
                pdfDoc.Save(pdfStream, saveOptions);
                pdfStream.Position = 0; // Reset stream position for reading

                // Initialize the PDF converter facade
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the modified PDF stream to the converter
                    converter.BindPdf(pdfStream);
                    // Prepare the converter for image extraction
                    converter.DoConvert();

                    int pageIndex = 1;
                    // Extract each page as a JPEG image
                    while (converter.HasNextImage())
                    {
#pragma warning disable CA1416 // Suppress platform‑specific warning for ImageFormat.Jpeg
                        string outputPath = Path.Combine(outputDirectory, $"page_{pageIndex}.jpg");
                        converter.GetNextImage(outputPath, ImageFormat.Jpeg);
#pragma warning restore CA1416
                        pageIndex++;
                    }
                }
            }
        }

        Console.WriteLine("PDF conversion to JPEG images completed.");
    }
}
