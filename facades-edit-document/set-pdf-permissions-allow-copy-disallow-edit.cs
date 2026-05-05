using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_protected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the source PDF to the security facade and apply privileges
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
            {
                fileSecurity.BindPdf(inputPath);

                // Start from a privilege set that allows everything, then restrict the required actions
                DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
                privilege.AllowCopy = true;                 // Allow copying text/graphics
                privilege.AllowFillIn = false;              // Disallow editing form fields
                privilege.AllowModifyAnnotations = false;   // Disallow adding/modifying annotations

                // Apply the privilege configuration
                fileSecurity.SetPrivilege(privilege);

                // Save the protected PDF
                fileSecurity.Save(outputPath);
            }

            Console.WriteLine($"Protected PDF saved to '{Path.GetFullPath(outputPath)}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
