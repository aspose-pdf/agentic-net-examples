using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for PNG output

class Program
{
    static void Main()
    {
        const string psInputPath = "input.ps";          // Source PostScript file
        const string tempPdfPath = "temp.pdf";          // Intermediate PDF file
        const string outputFolder = "PngPages";        // Folder for PNG images

        // Verify source file exists
        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"Source file not found: {psInputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // Step 1: Load the PS file and save it as PDF (required for PdfConverter)
        // -----------------------------------------------------------------
        using (Document psDoc = new Document(psInputPath, new PsLoadOptions()))
        {
            // Save the converted PDF to a temporary file
            psDoc.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Use PdfConverter (Facade) to convert each PDF page to PNG
        // -----------------------------------------------------------------
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the temporary PDF file
            converter.BindPdf(tempPdfPath);

            // Prepare for conversion
            converter.DoConvert();

            int pageIndex = 1;
            while (converter.HasNextImage())
            {
                // Build output file name for each page
                string pngPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");

                // Save current page as PNG
                converter.GetNextImage(pngPath, ImageFormat.Png);

                pageIndex++;
            }
        }

        // Clean up the temporary PDF file
        try
        {
            File.Delete(tempPdfPath);
        }
        catch
        {
            // Ignored – if deletion fails, the file will remain but does not affect conversion
        }

        Console.WriteLine("PS to PNG conversion completed successfully.");
    }
}