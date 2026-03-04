using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the encrypted source PDF and the decrypted output PDF.
        const string inputPdf  = @"C:\Docs\encrypted.pdf";
        const string outputPdf = @"C:\Docs\decrypted.pdf";

        // Owner (or user) password of the encrypted PDF.
        const string ownerPassword = "ownerPass";

        // Ensure the source file exists before attempting decryption.
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfFileSecurity works with file paths directly – no need for a Document instance.
            // The constructor receives the source (encrypted) file and the destination (decrypted) file.
            PdfFileSecurity security = new PdfFileSecurity(inputPdf, outputPdf);

            // DecryptFile returns true on success; it throws on failure, so we can also
            // wrap it in a try/catch to handle unexpected errors.
            bool result = security.DecryptFile(ownerPassword);

            if (result)
                Console.WriteLine($"Decryption succeeded. Decrypted file saved to '{outputPdf}'.");
            else
                Console.WriteLine("Decryption failed. Check the password and file integrity.");
        }
        catch (Exception ex)
        {
            // Handle any exceptions thrown by Aspose.Pdf.Facades (e.g., wrong password, I/O errors).
            Console.Error.WriteLine($"Error during decryption: {ex.Message}");
        }
    }
}