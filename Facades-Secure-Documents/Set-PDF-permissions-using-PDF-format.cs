using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileSecurity, DocumentPrivilege, PdfFileInfo

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF with modified permissions
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Set PDF permissions using Aspose.Pdf.Facades (PdfFileSecurity)
        // -----------------------------------------------------------------
        // PdfFileSecurity constructor takes the input and output file names.
        // SetPrivilege with a predefined DocumentPrivilege (e.g., Print) will
        // apply the corresponding permission set and write the result to
        // outputPath. No user/owner passwords are required; empty passwords
        // are used internally.
        // -----------------------------------------------------------------
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Apply the "Print" privilege (allows printing, forbids other actions)
        bool setResult = fileSecurity.SetPrivilege(DocumentPrivilege.Print);

        if (!setResult)
        {
            Console.Error.WriteLine("Failed to set PDF permissions.");
            return;
        }

        // Optional: verify the applied privileges using PdfFileInfo
        PdfFileInfo fileInfo = new PdfFileInfo(outputPath);
        var privilege = fileInfo.GetDocumentPrivilege();

        Console.WriteLine("Permissions applied:");
        Console.WriteLine($"AllowPrint: {privilege.AllowPrint}");
        Console.WriteLine($"AllowCopy: {privilege.AllowCopy}");
        Console.WriteLine($"AllowModifyContents: {privilege.AllowModifyContents}");
        // Add more checks as needed

        Console.WriteLine($"Output PDF with updated permissions saved to '{outputPath}'.");
    }
}