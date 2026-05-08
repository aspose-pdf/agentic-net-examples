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
                // Decrypt the document (owner password already supplied)
                doc.Decrypt();

                // Modify document metadata
                doc.Info.Title = "Updated Title";
                doc.Info.Author = "New Author";
                doc.Info.Subject = "Updated Subject";
                doc.Info.Keywords = "Aspose, PDF, Metadata";

                // Save the decrypted and updated PDF
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