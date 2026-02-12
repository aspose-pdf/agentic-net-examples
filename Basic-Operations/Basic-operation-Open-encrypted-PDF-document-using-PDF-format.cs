using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Paths for the encrypted source PDF and the decrypted output PDF
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";

        // Password required to open the encrypted PDF (user password)
        const string password = "userpwd";

        // Verify that the source file exists before attempting to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the password constructor
            Document pdfDocument = new Document(inputPath, password);

            // Save the opened (now decrypted) document to a new file
            // This uses the provided document-save rule: {DocumentVar}.Save({OutputPath});
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Decrypted PDF saved to: {outputPath}");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Invalid password supplied for the encrypted PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}