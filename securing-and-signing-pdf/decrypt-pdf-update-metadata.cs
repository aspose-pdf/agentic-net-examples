using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted.pdf";
        const string outputPath = "decrypted_modified.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the encrypted PDF using the owner password.
        // The password is supplied via the Document constructor.
        using (Document doc = new Document(inputPath, ownerPassword))
        {
            // Remove encryption. Decrypt() takes no parameters.
            doc.Decrypt();

            // Modify document metadata.
            doc.Info.Title  = "Updated Title";
            doc.Info.Author = "Updated Author";
            doc.Info.Subject = "Updated Subject";
            doc.Info.Keywords = "Aspose, PDF, Metadata";

            // Save the decrypted and updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
    }
}