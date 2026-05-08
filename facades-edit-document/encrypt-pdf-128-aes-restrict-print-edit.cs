using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPdf))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPdf);
            }
        }

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Define privileges: allow everything except printing and content modification
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowPrint          = false; // restrict printing
        privilege.AllowModifyContents = false; // restrict editing

        // Create a PdfFileSecurity instance and bind the source PDF
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(inputPdf);

        // Encrypt using 128‑bit AES
        fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x128, Algorithm.AES);

        // Save the encrypted PDF to the output file
        fileSecurity.Save(outputPdf);

        Console.WriteLine($"PDF encrypted and saved to '{outputPdf}'.");
    }
}
