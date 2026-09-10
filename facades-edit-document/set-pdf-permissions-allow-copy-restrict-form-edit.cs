using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_output.pdf";

        // Optional passwords – empty strings mean no password protection
        const string userPassword  = "";
        const string ownerPassword = "";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a privilege object based on the predefined "AllowAll"
        // then disable form filling and annotation modification.
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowCopy               = true;   // allow copying text/content
        privilege.AllowFillIn             = false;  // prevent editing form fields
        privilege.AllowModifyAnnotations = false;  // prevent adding/modifying annotations

        // Initialize the PdfFileSecurity facade with input and output files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Apply the privilege settings (no passwords in this example)
        bool success = fileSecurity.SetPrivilege(userPassword, ownerPassword, privilege);
        if (!success)
        {
            Console.Error.WriteLine("Failed to set PDF permissions.");
        }

        // Close the facade to release resources
        fileSecurity.Close();

        Console.WriteLine($"PDF saved with updated permissions to '{outputPath}'.");
    }
}