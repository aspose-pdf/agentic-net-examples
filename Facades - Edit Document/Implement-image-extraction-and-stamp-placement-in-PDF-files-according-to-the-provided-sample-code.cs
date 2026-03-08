using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output PDF and the folder where extracted images will be saved
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "stamped_output.pdf";
        const string imagesFolder = "ExtractedImages";

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        // -------------------------------------------------
        // 0. Create a simple source PDF if it does not exist (prevents FileNotFoundException)
        // -------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            var dummyDoc = new Document();
            dummyDoc.Pages.Add(); // add a blank page
            dummyDoc.Save(inputPdfPath);
        }

        // -------------------------------------------------
        // 1. Extract all images from the source PDF file
        // -------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Enable image extraction mode
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each image and save it to the images folder
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imagesFolder, $"image-{imageIndex}.png");
                // Save the next image to the specified file
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }

            // Close the extractor (releases the bound PDF)
            extractor.Close();

            // -------------------------------------------------
            // 2. If at least one image was extracted, use it as a stamp
            // -------------------------------------------------
            if (imageIndex > 1) // at least one image was saved
            {
                // Create a stamp and bind the first extracted image (fully qualified to avoid ambiguity)
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                string firstImagePath = Path.Combine(imagesFolder, "image-1.png");
                stamp.BindImage(firstImagePath);

                // Position and size of the stamp on each page
                stamp.SetOrigin(100, 500);          // X = 100, Y = 500 (bottom‑left origin)
                stamp.SetImageSize(100, 100);       // Width = 100, Height = 100
                stamp.Opacity = 0.7f;               // 70% opacity
                stamp.IsBackground = false;         // Place stamp on top of page content

                // Load the source PDF into a Document (required by the non‑obsolete PdfFileStamp ctor)
                Document srcDoc = new Document(inputPdfPath);

                // Use the non‑obsolete constructor (Document source) and save to destination explicitly
                using (PdfFileStamp fileStamp = new PdfFileStamp(srcDoc))
                {
                    fileStamp.AddStamp(stamp);
                    // Save the stamped document to the output path
                    fileStamp.Save(outputPdfPath);
                }

                Console.WriteLine($"Images extracted to '{imagesFolder}' and stamp applied. Output saved to '{outputPdfPath}'.");
            }
            else
            {
                Console.WriteLine("No images were found in the source PDF; stamp not applied.");
            }
        }
    }
}
