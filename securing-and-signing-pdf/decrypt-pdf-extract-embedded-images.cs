using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";   // input encrypted PDF
        const string userPassword     = "user123";        // user password for decryption
        const string outputFolder     = "ExtractedImages"; // folder to store extracted images

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Open the encrypted PDF with the user password using a stream overload
            using (FileStream pdfStream = new FileStream(encryptedPdfPath, FileMode.Open, FileAccess.Read))
            using (Document doc = new Document(pdfStream, userPassword))
            {
                // Remove encryption from the document
                doc.Decrypt();

                int imageCounter = 1;

                // Iterate over all pages
                foreach (Page page in doc.Pages)
                {
                    // Iterate over all images on the current page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Save each image as PNG using a FileStream (XImage.Save expects a Stream)
                        string imagePath = Path.Combine(outputFolder, $"image_{imageCounter}.png");
                        using (FileStream imgStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(imgStream);
                        }
                        Console.WriteLine($"Saved image {imageCounter} to '{imagePath}'");
                        imageCounter++;
                    }
                }
            }

            Console.WriteLine("Image extraction completed successfully.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("The provided password is incorrect.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
