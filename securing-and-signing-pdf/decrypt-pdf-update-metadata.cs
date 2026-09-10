using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted_updated.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the owner password
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Remove encryption from the document
                doc.Decrypt();

                // Modify document metadata
                doc.Info.Title = "Updated Document Title";
                doc.Info.Author = "John Doe";
                doc.Info.Subject = "Decrypted and updated metadata";
                doc.Info.Keywords = "Aspose.Pdf, Decrypt, Metadata";

                // Save the decrypted PDF with the new metadata
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