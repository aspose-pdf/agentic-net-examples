using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be watermarked
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Path to the watermark image (PNG, JPG, etc.)
        const string watermarkImagePath = "watermark.png";
        // Directory where watermarked PDFs will be saved
        const string outputDirectory = "Watermarked";

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Create a single ImageStamp instance that will be reused for all PDFs.
        // Reusing the same stamp keeps the image data embedded only once per document,
        // reducing the overall file size compared to loading a new stamp each time.
        Aspose.Pdf.ImageStamp sharedStamp = new Aspose.Pdf.ImageStamp(watermarkImagePath)
        {
            // Place the stamp behind the page content
            Background = true,
            // Semi‑transparent watermark
            Opacity = 0.5,
            // Center the image on each page
            HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
            VerticalAlignment   = Aspose.Pdf.VerticalAlignment.Center
        };

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {pdfPath}");
                continue;
            }

            // Load the source PDF inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
            {
                // Apply the shared watermark to every page of the document
                foreach (Aspose.Pdf.Page page in doc.Pages)
                {
                    page.AddStamp(sharedStamp);
                }

                // Build the output file path
                string outputPath = Path.Combine(outputDirectory, Path.GetFileName(pdfPath));

                // Save the watermarked PDF
                doc.Save(outputPath);
                Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
            }
        }
    }
}