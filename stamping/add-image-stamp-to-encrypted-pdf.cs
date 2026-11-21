using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string userPassword      = "user123";
        const string stampImagePath    = "stamp.png";
        const string outputPdfPath     = "stamped_output.pdf";

        // Verify required files exist
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"Encrypted PDF not found: {encryptedPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        try
        {
            // Open the encrypted PDF with the user password
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Decrypt the document (no parameters)
                doc.Decrypt();

                // Create an image stamp and configure its appearance
                ImageStamp imgStamp = new ImageStamp(stampImagePath)
                {
                    // Example settings – adjust as needed
                    Background          = false,
                    Opacity             = 0.5,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Apply the stamp to each page individually (PageCollection has no AddStamp)
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Incorrect password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}