using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "filled.pdf";          // source PDF (already filled)
        const string outputPath = "protected.pdf";      // final password‑protected PDF
        const string userPassword = "user123";          // password required to open
        const string ownerPassword = "owner123";        // password required for permissions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF (if any further modifications are needed) and save it.
        // Using block guarantees deterministic disposal of the Document object.
        using (Document doc = new Document(inputPath))
        {
            // No additional changes are made here; just ensure the document is a valid PDF.
            // Save to a temporary file because PdfFileSecurity works with file paths.
            doc.Save(outputPath);
        }

        // Apply password protection using the PdfFileSecurity facade.
        // The facade does not implement IDisposable, so it is not wrapped in a using block.
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(outputPath); // bind the just‑saved PDF

        // Set the desired privileges and passwords.
        // DocumentPrivilege.Print allows printing; adjust as needed.
        bool protectedOk = fileSecurity.SetPrivilege(userPassword, ownerPassword, DocumentPrivilege.Print);
        if (!protectedOk)
        {
            Console.Error.WriteLine("Failed to apply PDF security.");
            return;
        }

        // Save the protected PDF (overwrites the previous file).
        fileSecurity.Save(outputPath);

        Console.WriteLine($"Password‑protected PDF saved to '{outputPath}'.");
    }
}