using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the encrypted input PDF and the decrypted output PDF
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";

        // Password can be the owner password or the user (open) password
        const string password = "ownerpass";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSecurity facade with input and output file names
        // This constructor does not create a Document directly; it prepares the facade for security operations.
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Decrypt the PDF using the provided password.
        // DecryptFile returns true on success; otherwise decryption failed (e.g., wrong password).
        bool success = fileSecurity.DecryptFile(password);

        if (success)
        {
            Console.WriteLine($"Decryption succeeded. Decrypted file saved as '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Decryption failed. Verify that the password is correct.");
        }

        // Release resources held by the facade.
        fileSecurity.Close();
    }
}