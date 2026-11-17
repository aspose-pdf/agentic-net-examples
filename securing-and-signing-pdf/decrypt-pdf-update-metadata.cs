using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted.pdf";   // PDF encrypted with owner password
        const string outputPath     = "decrypted_modified.pdf";
        const string ownerPassword  = "owner123";        // Owner password for decryption

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the owner password
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Remove encryption from the document
                doc.Decrypt();   // Decrypt() takes no parameters

                // Modify document metadata
                doc.Info.Title  = "Updated Title";
                doc.Info.Author = "Updated Author";
                doc.Info.Subject = "Updated Subject";
                doc.Info.Keywords = "Aspose.Pdf, Decryption, Metadata";

                // Save the decrypted and modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}