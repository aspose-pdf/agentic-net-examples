using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the high‑resolution BMP source, and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string bmpSourcePath = "highres.bmp";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bmpSourcePath))
        {
            Console.Error.WriteLine($"BMP source not found: {bmpSourcePath}");
            return;
        }

        // Create a temporary file to hold the BMP stream because ReplaceImage expects a file path
        string tempBmpPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".bmp");
        try
        {
            // Copy the BMP data into the temporary file
            using (FileStream sourceStream = File.OpenRead(bmpSourcePath))
            using (FileStream tempStream   = new FileStream(tempBmpPath, FileMode.Create, FileAccess.Write))
            {
                sourceStream.CopyTo(tempStream);
            }

            // Use PdfContentEditor (a Facade) to replace the image on page 1, index 1
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the existing PDF document
                editor.BindPdf(inputPdfPath);

                // Replace the first image on the first page with the BMP file
                // pageNumber = 1 (first page), index = 1 (first image on that page)
                editor.ReplaceImage(pageNumber: 1, index: 1, imageFile: tempBmpPath);

                // Save the modified PDF to the desired output location
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Image successfully replaced. Output saved to '{outputPdfPath}'.");
        }
        finally
        {
            // Clean up the temporary BMP file
            if (File.Exists(tempBmpPath))
            {
                try { File.Delete(tempBmpPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}