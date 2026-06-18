using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string filledPdfPath = "filled.pdf";
        const string protectedPdfPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(filledPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {filledPdfPath}");
            return;
        }

        // Initialize the PdfFileSecurity facade
        PdfFileSecurity fileSecurity = new PdfFileSecurity();

        // Bind the existing filled PDF
        fileSecurity.BindPdf(filledPdfPath);

        // Encrypt the PDF with user/owner passwords, set desired privileges and key size
        fileSecurity.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);

        // Save the encrypted PDF to a new file
        fileSecurity.Save(protectedPdfPath);

        // Release resources
        fileSecurity.Close();

        Console.WriteLine($"Encrypted PDF saved to '{protectedPdfPath}'.");
    }
}