using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class InsertImagesExample
{
    static void Main()
    {
        // Input PDF and output PDF paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_images.pdf";

        // Image files to insert and their target pages/positions
        // Each tuple: (imagePath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY)
        var imagesToInsert = new (string ImagePath, int Page, float LLX, float LLY, float URX, float URY)[]
        {
            ("image1.jpg", 1, 50f, 500f, 250f, 700f),   // Image on page 1
            ("image2.png", 2, 100f, 100f, 300f, 300f)   // Image on page 2
        };

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Process the PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize PdfFileMend with the loaded document
            PdfFileMend mender = new PdfFileMend(pdfDoc);

            // Insert each image
            foreach (var item in imagesToInsert)
            {
                // Verify image file exists
                if (!File.Exists(item.ImagePath))
                {
                    Console.Error.WriteLine($"Image file not found: {item.ImagePath}");
                    continue;
                }

                // Open the image as a stream (preserves original quality)
                using (FileStream imgStream = File.OpenRead(item.ImagePath))
                {
                    // AddImage adds the image to the specified page and rectangle.
                    // The rectangle coordinates are in points (1/72 inch) relative to the page.
                    // The method returns true on success; we ignore the return value here.
                    mender.AddImage(imgStream,
                                    item.Page,
                                    item.LLX,
                                    item.LLY,
                                    item.URX,
                                    item.URY);
                }
            }

            // Save the modified PDF. Document.Save(string) without SaveOptions always writes PDF.
            mender.Save(outputPdfPath);

            // Close the PdfFileMend instance (releases internal resources)
            mender.Close();
        }

        Console.WriteLine($"Images inserted and saved to '{outputPdfPath}'.");
    }
}