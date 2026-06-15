using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (with the existing user password if any)
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Initialize the security facade on the loaded document
            PdfFileSecurity security = new PdfFileSecurity(doc);

            // Disable internal exception handling so that errors are thrown
            security.AllowExceptions = false;

            // Define the desired privilege (example: allow printing only)
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Apply the privilege; this will throw if the operation fails
            security.SetPrivilege(userPassword, ownerPassword, privilege);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Privilege modified and saved to '{outputPath}'.");
    }
}