using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchBookletCreator
{
    // A5 page size in points (1 point = 1/72 inch, 1 mm ≈ 2.83465 points)
    private const double A5WidthPoints  = 148 * 2.83465; // ≈ 419.53
    private const double A5HeightPoints = 210 * 2.83465; // ≈ 595.28

    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder  = @"C:\InputPdfs";
        // Folder where final booklets will be saved
        const string outputFolder = @"C:\OutputBooklets";
        // Temporary folder for intermediate resized PDFs
        const string tempFolder   = @"C:\TempResized";

        // Ensure output and temp directories exist
        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(tempFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Build paths for the intermediate resized PDF and the final booklet
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string resizedPath = Path.Combine(tempFolder, fileNameWithoutExt + "_A5.pdf");
                string bookletPath = Path.Combine(outputFolder, fileNameWithoutExt + "_booklet.pdf");

                // Resize the PDF pages to A5 dimensions
                PdfFileEditor editor = new PdfFileEditor();
                // 'null' for pages means all pages are processed
                editor.ResizeContents(inputPath, resizedPath, null, A5WidthPoints, A5HeightPoints);

                // Create a booklet from the resized PDF, keeping A5 as the output page size
                editor.MakeBooklet(resizedPath, bookletPath, PageSize.A5);

                // Clean up the intermediate file
                if (File.Exists(resizedPath))
                {
                    File.Delete(resizedPath);
                }

                Console.WriteLine($"Processed '{Path.GetFileName(inputPath)}' → booklet saved as '{Path.GetFileName(bookletPath)}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(inputPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}