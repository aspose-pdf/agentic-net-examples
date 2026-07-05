using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // For ImageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";   // Encrypted PDF file
        const string outputPath     = "stamped_output.pdf";    // Resulting PDF
        const string userPassword   = "user123";               // Password to open the PDF
        const string stampImagePath = "logo.png";               // Image to use as stamp

        // Verify files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Open the encrypted PDF using the correct password.
        // Document(string, string) constructor handles decryption internally.
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Create an ImageStamp from the image file.
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Optional visual settings
                Background          = false,   // Stamp appears on top of page content
                Opacity             = 0.7f,    // Semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF. The document remains encrypted with the same password.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}