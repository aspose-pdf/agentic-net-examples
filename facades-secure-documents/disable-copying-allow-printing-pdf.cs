using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a privilege object that allows printing but forbids copying.
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowCopy = false;   // disable copying
        privilege.AllowPrint = true;   // ensure printing is allowed

        // Apply the privilege using PdfFileSecurity (Facades API).
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Load the source PDF.
            security.BindPdf(inputPath);

            // Set the defined privilege. Returns true on success.
            bool result = security.SetPrivilege(privilege);
            if (!result)
            {
                Console.Error.WriteLine("Failed to set document privileges.");
                return;
            }

            // Save the secured PDF.
            security.Save(outputPath);
        }

        Console.WriteLine($"Secured PDF saved to '{outputPath}'.");
    }
}