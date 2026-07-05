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

        // Apply password protection to the filled PDF
        using (PdfFileSecurity security = new PdfFileSecurity(filledPdfPath, protectedPdfPath))
        {
            // Set privilege (e.g., allow printing) with the specified passwords
            bool result = security.SetPrivilege(userPassword, ownerPassword, DocumentPrivilege.Print);
            if (!result)
            {
                Console.Error.WriteLine("Failed to apply PDF security.");
            }
        }

        Console.WriteLine($"Password-protected PDF saved to '{protectedPdfPath}'.");
    }
}