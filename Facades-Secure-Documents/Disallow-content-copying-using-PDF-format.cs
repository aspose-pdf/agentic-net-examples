using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the protected output PDF.
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_copy.pdf";

        // Verify that the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF document.
            using (Document pdfDoc = new Document(inputPath))
            {
                // Create a privilege object that allows everything except copying.
                DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
                privilege.AllowCopy = false; // Disallow content copying.

                // Initialize the PdfFileSecurity facade and bind the loaded document.
                PdfFileSecurity security = new PdfFileSecurity();
                security.BindPdf(pdfDoc);

                // Apply the privilege settings. No user or owner passwords are set (null).
                security.SetPrivilege(null, null, privilege);

                // Save the protected PDF. The resulting file will have copying disabled.
                security.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with copying disabled: {outputPath}");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}