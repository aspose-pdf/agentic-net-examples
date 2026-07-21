using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, an image to insert, and the resulting PDF.
        const string sourcePdfPath   = "source.pdf";
        const string imagePath       = "logo.png";
        const string destinationPath = "modified.pdf";

        // Verify that the required files exist.
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Create a PdfFileMend facade and bind it to the source PDF.
            PdfFileMend pdfMend = new PdfFileMend();
            pdfMend.BindPdf(sourcePdfPath);

            // Insert the image onto page 1 at the specified rectangle.
            // Coordinates are: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // AddImage(stream, pageNumber, llx, lly, urx, ury)
                pdfMend.AddImage(imgStream, 1, 100, 500, 300, 700);
            }

            // Save the modified PDF to the destination path.
            pdfMend.Save(destinationPath);

            // Close the facade to release any file handles.
            pdfMend.Close();

            Console.WriteLine($"Modified PDF saved to '{destinationPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}