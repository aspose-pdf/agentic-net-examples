using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_protected.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with input and output files
            PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);

            // Start from a privilege set that allows everything
            DocumentPrivilege privilege = DocumentPrivilege.AllowAll;

            // Prohibit copying of content while keeping other permissions enabled
            privilege.AllowCopy = false;   // false = copying forbidden
            // All other permissions (print, modify, etc.) remain true by default

            // Apply the privilege settings to the PDF
            bool success = security.SetPrivilege(privilege);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set document privileges.");
                return;
            }

            // Save the protected PDF (output path was already supplied in the constructor,
            // but calling Save explicitly ensures the file is written)
            security.Save(outputPath);

            Console.WriteLine($"PDF saved with copy protection: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}