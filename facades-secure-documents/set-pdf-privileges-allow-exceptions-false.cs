using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use a using block for deterministic disposal of the facade
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Bind the source PDF
            security.BindPdf(inputPdf);

            // Disable internal exception handling so that failures throw
            security.AllowExceptions = false;

            // Set the desired privilege (example: allow printing only)
            // This method throws if the operation fails because AllowExceptions is false
            security.SetPrivilege(userPassword, ownerPassword, DocumentPrivilege.Print);

            // Save the modified PDF to the output path
            security.Save(outputPdf);
        }

        Console.WriteLine($"Privilege modification completed. Output saved to '{outputPdf}'.");
    }
}