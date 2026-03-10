using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source (protected) PDF and the destination PDF.
        const string inputPdf  = @"C:\Docs\protected_input.pdf";
        const string outputPdf = @"C:\Docs\protected_output.pdf";

        // Existing owner password of the source PDF.
        const string currentOwnerPassword = "oldOwnerPass";

        // New passwords to be applied.
        const string newUserPassword  = "newUserPass";
        const string newOwnerPassword = "newOwnerPass";

        // Verify that the source file exists before attempting any operation.
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the protected PDF into an Aspose.Pdf.Document instance.
        Document pdfDoc = new Document(inputPdf);

        // Bind the document to the PdfFileSecurity facade.
        // The constructor expects a Document, not a file path.
        PdfFileSecurity security = new PdfFileSecurity(pdfDoc);

        // Change the password while preserving the original security settings.
        // Overload: ChangePassword(string ownerPassword, string newUserPassword, string newOwnerPassword)
        bool changed = security.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);

        if (changed)
        {
            // Persist the updated protection to the output file.
            security.Save(outputPdf);
            Console.WriteLine("Password successfully updated.");
        }
        else
        {
            Console.WriteLine("Password change failed.");
        }
    }
}
