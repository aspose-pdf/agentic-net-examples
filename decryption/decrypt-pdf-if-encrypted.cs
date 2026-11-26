using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted_input.pdf";
        const string outputPath = "output.pdf";
        const string password   = "userPassword"; // password for encrypted PDFs; leave empty if unknown

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF; password is required only if the document is encrypted.
            using (Document doc = new Document(inputPath, password))
            {
                // Check whether the document is encrypted.
                if (doc.IsEncrypted)
                {
                    // Decrypt the document. Decrypt() takes no parameters.
                    doc.Decrypt();
                }

                // Save the (decrypted or unchanged) document to the output path.
                doc.Save(outputPath);
            }

            Console.WriteLine($"File saved to '{outputPath}'.");
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