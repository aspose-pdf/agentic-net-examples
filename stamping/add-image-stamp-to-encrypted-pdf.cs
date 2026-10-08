using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdf = "encrypted.pdf";
        const string password = "user123";
        const string stampImage = "stamp.png";
        const string outputPdf = "stamped.pdf";

        if (!File.Exists(encryptedPdf))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the provided password
            using (Document doc = new Document(encryptedPdf, password))
            {
                // Decrypt the document (no parameters required)
                doc.Decrypt();

                // Create an ImageStamp with desired properties
                ImageStamp imgStamp = new ImageStamp(stampImage)
                {
                    Background = false,
                    Opacity = 0.5,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Apply the stamp to each page individually
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
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