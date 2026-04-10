using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the PdfFileSecurity facade
        PdfFileSecurity security = new PdfFileSecurity();
        security.BindPdf(inputPath);

        // Create a privilege based on AllowAll and then restrict editing and annotations
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowFillIn = false;               // Prevent form field editing
        privilege.AllowModifyAnnotations = false;    // Prevent annotation modifications
        privilege.AllowCopy = true;                  // Ensure copying text is allowed

        // Apply the privilege (no passwords are set)
        security.SetPrivilege(privilege);

        // Save the secured PDF
        security.Save(outputPath);
        security.Close();

        Console.WriteLine($"Permissions set and saved to '{outputPath}'.");
    }
}