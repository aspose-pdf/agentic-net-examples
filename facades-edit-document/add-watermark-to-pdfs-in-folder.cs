using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Configuration settings
        const string inputFolder  = @"C:\PdfInput";          // Folder containing source PDFs
        const string outputFolder = @"C:\PdfOutput";         // Folder for watermarked PDFs
        const string watermarkText = "CONFIDENTIAL";          // Watermark text
        const double fontSize = 48;                          // Font size for the watermark
        const double opacity = 0.3;                          // Opacity (0 = transparent, 1 = opaque)
        const double rotation = 45;                         // Rotation angle in degrees

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                string fileName   = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_watermarked.pdf");

                // Load the PDF document
                using (Document pdfDoc = new Document(inputPath))
                {
                    // Create a watermark artifact that will be added to every page
                    WatermarkArtifact watermark = new WatermarkArtifact();
                    watermark.SetTextAndState(watermarkText, new TextState
                    {
                        FontSize = (float)fontSize,
                        ForegroundColor = Color.FromRgb(0.8f, 0.8f, 0.8f), // Light gray
                        Font = FontRepository.FindFont("Arial") // optional – falls back to default if not found
                    });
                    watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                    watermark.ArtifactVerticalAlignment   = VerticalAlignment.Center;
                    watermark.Rotation = (float)rotation;
                    watermark.Opacity  = (float)opacity;
                    watermark.IsBackground = true; // render behind page content

                    // Add the watermark to each page
                    foreach (Page page in pdfDoc.Pages)
                    {
                        page.Artifacts.Add(watermark);
                    }

                    // Save the watermarked PDF
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Watermarked: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{inputPath}': {ex.Message}");
            }
        }
    }
}
