using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted.pdf";   // Path to the encrypted PDF
        const string outputPath = "decrypted_updated.pdf"; // Path for the decrypted PDF with updated metadata
        const string ownerPassword = "owner123";    // Owner password for decryption

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted document using the owner password
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Decrypt the document (removes encryption)
                doc.Decrypt();

                // Modify metadata via the DocumentInfo object
                doc.Info.Title  = "Updated Document Title";
                doc.Info.Author = "John Doe";
                doc.Info.Subject = "Decrypted and Updated PDF";
                doc.Info.Keywords = "Aspose.Pdf, Decryption, Metadata";

                // Save the decrypted, metadata‑updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}