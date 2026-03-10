using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Existing owner password (if the PDF is already protected)
        const string currentOwnerPassword = "oldOwnerPassword";

        // New passwords to assign
        const string newUserPassword  = "newUserPassword";
        const string newOwnerPassword = "newOwnerPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade
            PdfFileSecurity security = new PdfFileSecurity();

            // Bind the source PDF file
            security.BindPdf(inputPath);

            // Change the password while preserving existing security settings
            // Parameters: (ownerPassword, newUserPassword, newOwnerPassword)
            bool changed = security.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);

            if (!changed)
            {
                Console.Error.WriteLine("Password change failed.");
                return;
            }

            // Save the secured PDF to the output path
            security.Save(outputPath);

            Console.WriteLine($"PDF secured and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}