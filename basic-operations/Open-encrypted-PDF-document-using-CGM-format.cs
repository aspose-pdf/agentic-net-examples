using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF and its user password
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword = "user123";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Open the encrypted PDF using the password constructor
        using (Document pdfDoc = new Document(encryptedPdfPath, userPassword))
        {
            Console.WriteLine($"Encrypted PDF opened. Page count: {pdfDoc.Pages.Count}");

            // Decrypt the document (Decrypt takes no parameters)
            pdfDoc.Decrypt();

            // Save a decrypted copy (optional)
            pdfDoc.Save("decrypted_copy.pdf");
        }

        // -----------------------------------------------------------------
        // Example of loading a CGM file (input format) and converting it to PDF
        // This demonstrates using the CGM format load options.
        // -----------------------------------------------------------------
        const string cgmPath = "input.cgm";

        if (File.Exists(cgmPath))
        {
            // Use default CGM load options (A4 page size, 300 DPI)
            CgmLoadOptions cgmOptions = new CgmLoadOptions();

            // Load the CGM file and convert it to a PDF document
            using (Document cgmDoc = new Document(cgmPath, cgmOptions))
            {
                Console.WriteLine($"CGM file loaded. Page count: {cgmDoc.Pages.Count}");

                // Save the resulting PDF
                cgmDoc.Save("cgm_converted.pdf");
            }
        }
        else
        {
            Console.WriteLine("CGM file not found; skipping CGM conversion.");
        }
    }
}