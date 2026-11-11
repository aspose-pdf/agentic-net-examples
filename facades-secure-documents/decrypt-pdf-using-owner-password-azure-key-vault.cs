using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the encrypted and decrypted PDFs
        const string inputPdf = "encrypted.pdf";   // encrypted PDF to decrypt
        const string outputPdf = "decrypted.pdf"; // destination for the decrypted PDF

        // Retrieve the owner password from an environment variable.
        // In production replace this with a call to Azure Key Vault.
        string ownerPassword = Environment.GetEnvironmentVariable("PDF_OWNER_PASSWORD");
        if (string.IsNullOrEmpty(ownerPassword))
        {
            Console.WriteLine("Error: Owner password not found. Set the PDF_OWNER_PASSWORD environment variable.");
            return;
        }

        // Load the encrypted PDF using the owner password.
        Document doc;
        try
        {
            doc = new Document(inputPdf, ownerPassword);
        }
        catch (PdfException ex)
        {
            Console.WriteLine($"Failed to open encrypted PDF: {ex.Message}");
            return;
        }

        // Save the document without specifying a password – this produces an unencrypted PDF.
        doc.Save(outputPdf);
        Console.WriteLine($"Decrypted PDF saved to '{outputPdf}'.");
    }
}
