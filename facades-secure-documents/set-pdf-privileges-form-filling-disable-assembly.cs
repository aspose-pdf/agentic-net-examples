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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Start from a fully permissive privilege set
            DocumentPrivilege privilege = DocumentPrivilege.AllowAll;

            // Disable document assembly
            privilege.AllowAssembly = false;

            // Enable form filling
            privilege.AllowFillIn = true;

            // Apply the privilege settings using the Facades API
            using (PdfFileSecurity security = new PdfFileSecurity(doc))
            {
                security.SetPrivilege(privilege);
                security.Save(outputPath);
            }
        }

        Console.WriteLine($"Privileges updated and saved to '{outputPath}'.");
    }
}