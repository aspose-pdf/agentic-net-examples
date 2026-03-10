using System;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "secured.pdf";

        // Optional passwords – can be null or empty if not required
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify that the source file exists before proceeding
        if (!System.IO.File.Exists(inputPath))
        {
            Console.WriteLine($"Input file '{inputPath}' not found.");
            return;
        }

        // Use the parameter‑less constructor (the overload with destination is obsolete)
        PdfFileSecurity fileSecurity = new PdfFileSecurity();

        // Bind the source PDF document
        fileSecurity.BindPdf(inputPath);

        // Apply a predefined privilege (e.g., allow only printing)
        // This overload keeps the original passwords and sets the privilege.
        bool result = fileSecurity.SetPrivilege(userPassword, ownerPassword, DocumentPrivilege.Print);

        // Save the secured PDF to the destination path
        fileSecurity.Save(outputPath);

        // Report the outcome
        Console.WriteLine(result
            ? "Security settings applied successfully."
            : "Failed to apply security settings.");
    }
}