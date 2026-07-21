using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a minimal source PDF so the example can run in an empty sandbox.
        // ---------------------------------------------------------------------
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a simple PDF with one blank page.
        using (Document seed = new Document())
        {
            seed.Pages.Add();
            seed.Save(inputPath);
        }

        // ---------------------------------------------------------------------
        // 2. Modify the PDF privileges.
        // ---------------------------------------------------------------------
        // Original passwords (empty strings if the PDF is not protected).
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Use the parameter‑less constructor (the overload with destination is obsolete).
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        // Bind the source PDF.
        fileSecurity.BindPdf(inputPath);
        // No need to set AllowExceptions – the current API throws by default.

        // Choose the desired privilege (e.g., allow printing only).
        DocumentPrivilege privilege = DocumentPrivilege.Print;

        // This call will throw if the operation fails.
        fileSecurity.SetPrivilege(userPassword, ownerPassword, privilege);

        // Save the modified PDF to the destination path.
        fileSecurity.Save(outputPath);

        Console.WriteLine("Privilege modification completed successfully.");
    }
}
