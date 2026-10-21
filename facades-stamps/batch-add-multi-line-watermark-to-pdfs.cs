using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchWatermark
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where watermarked PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Define the multi‑line watermark text
        const string watermarkText = "Confidential\r\nDo Not Distribute\r\n© Company";

        // Iterate over all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the output file path (same file name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            // Load the source PDF using the high‑level Document API
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Create a TextStamp with the multi‑line text
                TextStamp textStamp = new TextStamp(watermarkText)
                {
                    RotateAngle = 45f,
                    Opacity = 0.5,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Configure appearance of the watermark
                textStamp.TextState.FontSize = 72;                                   // Large font
                textStamp.TextState.ForegroundColor = Color.Gray;                    // Gray color (cross‑platform)

                // Apply the stamp to every page of the PDF
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(textStamp);
                }

                // Save the watermarked PDF to the output location
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked: {outputPath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
