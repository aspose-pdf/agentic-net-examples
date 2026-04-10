using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileEditor
using Aspose.Pdf;          // PageSize

class BatchBookletProcessor
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder to store intermediate A5‑sized PDFs
        const string resizedFolder = "ResizedA5";
        // Folder to store final booklet PDFs
        const string outputFolder = "Booklets";

        // Ensure the output directories exist
        Directory.CreateDirectory(resizedFolder);
        Directory.CreateDirectory(outputFolder);

        // Verify that the input folder exists before trying to enumerate files
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No files to process.");
            return;
        }

        // Process every PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string resizedPath = Path.Combine(resizedFolder, fileNameWithoutExt + "_A5.pdf");
            string bookletPath = Path.Combine(outputFolder, fileNameWithoutExt + "_Booklet.pdf");

            // A5 page size in points (1 point = 1/72 inch, 1 mm = 2.83465 points)
            double a5Width  = 148.0 * 2.83465; // ≈ 419.53 points
            double a5Height = 210.0 * 2.83465; // ≈ 595.28 points

            // Resize all pages to A5 dimensions.
            // Passing null for the pages array means “all pages”.
            PdfFileEditor editor = new PdfFileEditor();
            editor.ResizeContents(inputPath, resizedPath, null, a5Width, a5Height);

            // Create a booklet from the resized PDF.
            // The output booklet will also use A5 page size.
            editor.MakeBooklet(resizedPath, bookletPath, PageSize.A5);

            Console.WriteLine($"Processed '{fileNameWithoutExt}': booklet saved to '{bookletPath}'.");
        }
    }
}
