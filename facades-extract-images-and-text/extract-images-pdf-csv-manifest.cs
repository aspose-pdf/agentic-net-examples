using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;               // <-- added for Document
using Aspose.Pdf.Facades;

class ImageExtractorWithManifest
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                 // source PDF
        const string outputDir = "ExtractedImages";         // folder for images
        const string csvPath = "image_manifest.csv";        // manifest file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Prepare CSV writer
        using (StreamWriter csvWriter = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            // Write CSV header
            csvWriter.WriteLine("FileName,PageNumber,Width,Height");

            // Load the PDF to know the total page count
            using (Document pdfDoc = new Document(pdfPath))
            {
                int totalPages = pdfDoc.Pages.Count;

                // Loop through each page and extract images from that page only
                for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
                {
                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        extractor.BindPdf(pdfPath);
                        // Restrict extraction to the current page
                        extractor.StartPage = pageNumber;
                        extractor.EndPage   = pageNumber;
                        // Extract only images
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            // Build a unique file name – we store everything as PNG for simplicity
                            string fileName = $"image_page{pageNumber}_idx{imageIndex}.png";
                            string filePath = Path.Combine(outputDir, fileName);

                            // Save the extracted image to disk
                            extractor.GetNextImage(filePath, ImageFormat.Png);

                            // Load the saved image to obtain its dimensions (fully qualified to avoid ambiguity)
                            using (System.Drawing.Image img = System.Drawing.Image.FromFile(filePath))
                            {
                                csvWriter.WriteLine($"{fileName},{pageNumber},{img.Width},{img.Height}");
                            }

                            imageIndex++;
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Extraction complete. Images saved to '{outputDir}'. Manifest saved to '{csvPath}'.");
    }
}
