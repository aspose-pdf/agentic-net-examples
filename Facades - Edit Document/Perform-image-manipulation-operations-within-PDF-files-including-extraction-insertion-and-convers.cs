using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat enum
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;          // Resolution enum
using Aspose.Pdf.Text;             // For any text handling if needed

class PdfImageManipulation
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string extractedImagesFolder = "ExtractedImages";
        const string imageToInsertPath = "insert.png";
        const string insertedPdfPath = "output_inserted.pdf";
        const string convertedImagesFolder = "ConvertedImages";

        // Ensure output directories exist
        Directory.CreateDirectory(extractedImagesFolder);
        Directory.CreateDirectory(convertedImagesFolder);

        try
        {
            // -------------------------------------------------
            // 1. Extract embedded images from the PDF
            // -------------------------------------------------
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);
                // PdfExtractor expects an integer DPI, not a Resolution object
                extractor.Resolution = 300; // higher DPI gives clearer images
                extractor.ExtractImage();

                int imgIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outImagePath = Path.Combine(
                        extractedImagesFolder,
                        $"extracted_{imgIndex}.png");

                    // Save each image as PNG (ImageFormat from System.Drawing.Imaging)
                    extractor.GetNextImage(outImagePath, ImageFormat.Png);
                    imgIndex++;
                }
            }

            // -------------------------------------------------
            // 2. Insert a new image into the PDF (page 1)
            // -------------------------------------------------
            using (PdfFileMend mend = new PdfFileMend())
            {
                mend.BindPdf(inputPdfPath);

                // AddImage(string imagePath, int pageNumber,
                //          float llx, float lly, float urx, float ury)
                // Coordinates are in points (1/72 inch). Adjust as needed.
                mend.AddImage(
                    imageToInsertPath,   // image file
                    1,                   // target page (1‑based)
                    100, 500, 300, 650); // lower‑left (100,500), upper‑right (300,650)

                mend.Save(insertedPdfPath);
            }

            // -------------------------------------------------
            // 3. Convert each PDF page to an image (PNG)
            // -------------------------------------------------
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(inputPdfPath);
                converter.StartPage = 1;
                converter.EndPage = converter.PageCount; // convert all pages
                // PdfConverter expects a Resolution object
                converter.Resolution = new Resolution(150); // reasonable quality

                // Prepare internal structures for conversion
                converter.DoConvert();

                int pageNumber = 1;
                while (converter.HasNextImage())
                {
                    string outPageImage = Path.Combine(
                        convertedImagesFolder,
                        $"page_{pageNumber}.png");

                    // Save the rendered page as PNG
                    converter.GetNextImage(outPageImage, ImageFormat.Png);
                    pageNumber++;
                }
            }

            Console.WriteLine("Image extraction, insertion, and conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
