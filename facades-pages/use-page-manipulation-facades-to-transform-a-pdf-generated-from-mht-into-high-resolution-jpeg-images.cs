using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution struct

class Program
{
    static void Main()
    {
        // Paths
        const string mhtPath   = "input.mht";          // Source MHT file
        const string pdfPath   = "intermediate.pdf";   // Temporary PDF file
        const string outputDir = "Images";             // Folder for JPEG images

        // Verify source file exists
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // -----------------------------------------------------------------
            // Step 1: Convert MHT (HTML) to PDF using Document with HtmlLoadOptions
            // -----------------------------------------------------------------
            using (Document pdfDoc = new Document(mhtPath, new HtmlLoadOptions()))
            {
                // Save the intermediate PDF
                pdfDoc.Save(pdfPath);
            }

            // -----------------------------------------------------------------
            // Step 2: Use PdfConverter (facade) to render each PDF page to a high‑resolution JPEG
            // -----------------------------------------------------------------
            using (Document pdfDoc = new Document(pdfPath))
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Set desired resolution (e.g., 300 DPI)
                converter.Resolution = new Resolution(300);

                // Prepare the converter
                converter.DoConvert();

                int pageNumber = 1;
                while (converter.HasNextImage())
                {
                    // Build output file name for each page
                    string jpegPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                    // Save the current page as JPEG (default format is JPEG)
                    converter.GetNextImage(jpegPath);

                    pageNumber++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
            return;
        }
        finally
        {
            // Optional: clean up the intermediate PDF file
            try { if (File.Exists(pdfPath)) File.Delete(pdfPath); } catch { /* ignore cleanup errors */ }
        }

        Console.WriteLine("Conversion completed successfully.");
    }
}
