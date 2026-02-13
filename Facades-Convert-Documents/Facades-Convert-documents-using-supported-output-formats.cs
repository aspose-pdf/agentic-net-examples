using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution used by PdfConverter

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file – change as needed
        const string inputPdf = "sample.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);

            // -----------------------------------------------------------------
            // 1. Convert to various document formats using the core Document API.
            //    The Save method infers the output format from the file extension.
            // -----------------------------------------------------------------
            ConvertAndSave(pdfDocument, Path.ChangeExtension(inputPdf, ".doc"));
            ConvertAndSave(pdfDocument, Path.ChangeExtension(inputPdf, ".docx"));
            ConvertAndSave(pdfDocument, Path.ChangeExtension(inputPdf, ".html"));
            ConvertAndSave(pdfDocument, Path.ChangeExtension(inputPdf, ".xps"));
            ConvertAndSave(pdfDocument, Path.ChangeExtension(inputPdf, ".svg"));
            ConvertAndSave(pdfDocument, Path.ChangeExtension(inputPdf, ".epub"));
            ConvertAndSave(pdfDocument, Path.ChangeExtension(inputPdf, ".pptx"));
            ConvertAndSave(pdfDocument, Path.ChangeExtension(inputPdf, ".xml"));
            ConvertAndSave(pdfDocument, Path.ChangeExtension(inputPdf, ".txt"));

            // -----------------------------------------------------------------
            // 2. Convert to image formats using the Facades PdfConverter.
            //    PdfConverter works cross‑platform and does not require System.Drawing.
            // -----------------------------------------------------------------
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded PDF document
                converter.BindPdf(pdfDocument);

                // Set desired resolution (e.g., 150 DPI)
                converter.Resolution = new Resolution(150);

                // Perform the conversion
                converter.DoConvert();

                // Save all pages as a single multi‑page TIFF file
                string tiffOutput = Path.ChangeExtension(inputPdf, ".tiff");
                converter.SaveAsTIFF(tiffOutput);
                Console.WriteLine($"Converted to TIFF: {tiffOutput}");

                // Save each page as a separate PNG file (optional)
                // Note: SaveAsPNG is not available in PdfConverter; use PngDevice if needed.
                // The example keeps the code cross‑platform by avoiding GDI+ dependent devices.
            }

            Console.WriteLine("All conversions completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Helper method that follows the prescribed document‑save rule.
    static void ConvertAndSave(Document doc, string outputPath)
    {
        // Ensure the output directory exists
        string? dir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        // Save the document – format is inferred from the file extension
        doc.Save(outputPath);
        Console.WriteLine($"Saved: {outputPath}");
    }
}