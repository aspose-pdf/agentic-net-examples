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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfFileSecurity facade and bind the document
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                security.BindPdf(doc);

                // Build a privilege set:
                // Start from a completely forbidden set, then enable only copying.
                DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
                privilege.AllowCopy = true;                 // Allow text/image copying
                privilege.AllowFillIn = false;               // Disallow form field editing
                privilege.AllowModifyAnnotations = false;    // Disallow annotation changes

                // Apply the privilege to the PDF (no passwords are set)
                security.SetPrivilege(privilege);

                // Save the secured PDF
                security.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with updated permissions to '{outputPath}'.");
    }
}