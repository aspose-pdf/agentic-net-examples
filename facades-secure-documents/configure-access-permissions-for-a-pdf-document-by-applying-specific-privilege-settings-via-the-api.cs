using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ConfigurePdfPermissions
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF with new privileges
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF using the PdfFileSecurity facade.
        //    The constructor (inputFile, outputFile) follows the provided
        //    create/load pattern for facades.
        // -----------------------------------------------------------------
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // -----------------------------------------------------------------
        // 2. (Optional) Inspect current privileges.
        //    PdfFileInfo can be used to read the existing DocumentPrivilege.
        // -----------------------------------------------------------------
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        DocumentPrivilege currentPrivilege = fileInfo.GetDocumentPrivilege();
        Console.WriteLine($"Current privilege: {currentPrivilege}");

        // -----------------------------------------------------------------
        // 3. Define the desired privilege.
        //    Example: allow only printing and disallow all other actions.
        //    DocumentPrivilege provides predefined static instances.
        // -----------------------------------------------------------------
        DocumentPrivilege newPrivilege = DocumentPrivilege.Print;

        // -----------------------------------------------------------------
        // 4. Apply the privilege to the PDF.
        //    SetPrivilege returns true on success.
        // -----------------------------------------------------------------
        bool success = fileSecurity.SetPrivilege(newPrivilege);
        if (!success)
        {
            Console.Error.WriteLine("Failed to set the new privilege.");
            return;
        }

        // -----------------------------------------------------------------
        // 5. The output file is written automatically by SetPrivilege.
        //    No explicit Save call is required for PdfFileSecurity.
        // -----------------------------------------------------------------
        Console.WriteLine($"Privileges updated successfully. Output saved to '{outputPath}'.");
    }
}