using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "encrypted_input.pdf";
        const string outputPdf = "decrypted_output.pdf";
        const string ownerPassword = "ownerPass"; // set to null/empty if not known

        // Ensure the input file exists before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfFileSecurity implements IDisposable, so wrap it in a using block
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Attempt to bind the PDF. If the file is encrypted and no password is supplied,
                // Aspose.Pdf throws InvalidPasswordException.
                security.BindPdf(inputPdf);

                // If binding succeeded, decrypt using the owner password (or user password if owner is null)
                security.DecryptFile(ownerPassword);

                // Save the decrypted PDF to the desired output path
                security.Save(outputPdf);
                Console.WriteLine($"Decryption successful. Output saved to '{outputPdf}'.");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Handle the specific case where BindPdf failed because the PDF is encrypted
            Console.Error.WriteLine("Error: The PDF is encrypted and could not be opened without a valid password.");
            Console.Error.WriteLine($"Details: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General fallback for any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}