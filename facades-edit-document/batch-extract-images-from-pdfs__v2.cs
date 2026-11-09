using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "PdfCollection";
        // Root folder where subfolders with extracted images will be created
        const string outputRoot = "ExtractedImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Create a subfolder named after the PDF (without extension)
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputDir = Path.Combine(outputRoot, pdfName);
            Directory.CreateDirectory(pdfOutputDir);

            // Use Aspose.Pdf.Facades.PdfExtractor to pull images out of the PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                // The ExtractImageMode property is not available in the referenced version of Aspose.Pdf.
                // The default behavior extracts all images, so we simply call ExtractImage().
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build the output file name
                    string imagePath = Path.Combine(pdfOutputDir, $"Image_{imageIndex}.png");
                    // GetNextImage overload requires a destination path and the desired format
                    extractor.GetNextImage(imagePath, ImageFormat.Png);
                    imageIndex++;
                }

                Console.WriteLine($"Extracted {imageIndex - 1} image(s) from '{pdfName}.pdf' to '{pdfOutputDir}'.");
            }
        }
    }
}
