using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a privilege object that forbids everything, then enable form filling and disable assembly.
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        privilege.AllowFillIn = true;      // allow filling form fields
        privilege.AllowAssembly = false;   // disable document assembly

        // Apply the privilege to the PDF using PdfFileSecurity.
        PdfFileSecurity security = new PdfFileSecurity();
        security.BindPdf(inputPath);
        security.SetPrivilege(privilege);
        security.Save(outputPath);
        // Close the facade if the method exists.
        try
        {
            security.Close();
        }
        catch (Exception)
        {
            // Ignore if Close is not available in this version.
        }

        Console.WriteLine($"Privileges applied and saved to '{outputPath}'.");
    }
}
