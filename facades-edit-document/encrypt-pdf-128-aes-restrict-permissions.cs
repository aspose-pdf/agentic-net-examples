using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for security
using Aspose.Pdf;           // Needed for DocumentPrivilege, KeySize, Algorithm

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "secured_output.pdf";

        // User and owner passwords (can be empty strings if not required)
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create a PdfFileSecurity facade with source and destination files
            PdfFileSecurity security = new PdfFileSecurity(inputPdf, outputPdf);

            // Define privileges: forbid all actions (no printing, no editing, etc.)
            DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;

            // Encrypt using 128‑bit AES (KeySize.x128) and AES algorithm
            bool success = security.EncryptFile(
                userPassword,
                ownerPassword,
                privilege,
                KeySize.x128,
                Algorithm.AES);

            // Close the facade (releases resources)
            security.Close();

            Console.WriteLine(success
                ? $"PDF encrypted successfully and saved to '{outputPdf}'."
                : "PDF encryption failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}