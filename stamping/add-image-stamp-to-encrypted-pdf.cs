using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageStamp (inherits from Stamp)
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath   = "encrypted_input.pdf";
        const string outputPdfPath  = "stamped_output.pdf";
        const string imagePath      = "stamp_image.png";
        const string userPassword   = "user123"; // password to open the encrypted PDF

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {imagePath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password
            using (Document doc = new Document(inputPdfPath, userPassword))
            {
                // Create an image stamp from the specified image file
                ImageStamp imgStamp = new ImageStamp(imagePath)
                {
                    // Position and appearance settings (optional)
                    Background = false,                     // stamp on top of page content
                    Opacity    = 0.7f,                      // semi‑transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Add the stamp to the first page (page indexing is 1‑based)
                doc.Pages[1].AddStamp(imgStamp);

                // Save the modified PDF (encryption is preserved)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}