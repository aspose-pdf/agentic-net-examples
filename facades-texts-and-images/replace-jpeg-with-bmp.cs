using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing a JPEG image on the first page
        string inputPdfPath = "input.pdf";
        // Output PDF after replacement
        string outputPdfPath = "output.pdf";
        // High‑resolution BMP image file (source for the replacement)
        string bmpSourcePath = "highres.bmp";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdfPath);
            return;
        }
        if (!File.Exists(bmpSourcePath))
        {
            Console.Error.WriteLine("BMP source image not found: " + bmpSourcePath);
            return;
        }

        // Create a temporary file to hold the BMP stream because PdfContentEditor.ReplaceImage expects a file path
        string tempBmpPath = Path.GetTempFileName();
        try
        {
            // Copy BMP data into the temporary file
            using (FileStream sourceStream = new FileStream(bmpSourcePath, FileMode.Open, FileAccess.Read))
            using (FileStream tempStream = new FileStream(tempBmpPath, FileMode.Create, FileAccess.Write))
            {
                sourceStream.CopyTo(tempStream);
            }

            // Use PdfContentEditor to replace the image on page 1, image index 1
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdfPath);
                // Replace the first image on the first page with the BMP file
                editor.ReplaceImage(1, 1, tempBmpPath);
                editor.Save(outputPdfPath);
            }

            Console.WriteLine("Image replaced successfully. Output saved to " + outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            // Clean up the temporary BMP file
            if (File.Exists(tempBmpPath))
            {
                try
                {
                    File.Delete(tempBmpPath);
                }
                catch
                {
                    // Ignored – best‑effort cleanup
                }
            }
        }
    }
}
