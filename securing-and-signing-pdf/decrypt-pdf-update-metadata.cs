using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted_modified.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF with the owner password
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Decrypt the document (no parameters)
                doc.Decrypt();

                // Modify metadata
                doc.Info.Title = "Updated Title";
                doc.Info.Author = "Updated Author";
                doc.Info.Subject = "Updated Subject";
                doc.Info.Keywords = "keyword1, keyword2";

                // Save the decrypted and updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decrypted and updated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}