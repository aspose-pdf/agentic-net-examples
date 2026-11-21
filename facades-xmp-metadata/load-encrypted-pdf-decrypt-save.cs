using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF (may be encrypted)
        const string outputPath = "output.pdf";  // destination PDF
        const string password   = "owner123";   // password for encrypted PDFs (if known)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileMend provides BindPdf and Save without loading the whole document into memory
        PdfFileMend mend = new PdfFileMend();

        try
        {
            // Attempt to bind the PDF directly
            mend.BindPdf(inputPath);
        }
        catch (InvalidPasswordException)
        {
            // The PDF is encrypted – try to open it with the supplied password, decrypt, and re‑bind
            Console.WriteLine("PDF is encrypted. Attempting decryption with provided password.");

            try
            {
                // Document implements IDisposable; wrap in using for deterministic disposal
                using (Document doc = new Document(inputPath, password))
                {
                    // Decrypt the document (Decrypt takes no parameters)
                    doc.Decrypt();

                    // Save the decrypted version to a temporary file
                    string tempPath = Path.GetTempFileName();
                    doc.Save(tempPath);

                    // Bind the temporary unencrypted PDF
                    mend.BindPdf(tempPath);

                    // Save the final result
                    mend.Save(outputPath);
                    Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");

                    // Clean up the temporary file
                    File.Delete(tempPath);
                }
                return;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to decrypt PDF: {ex.Message}");
                return;
            }
        }
        catch (Exception ex)
        {
            // Any other binding error (e.g., file corruption) is reported here
            Console.Error.WriteLine($"Failed to bind PDF: {ex.Message}");
            return;
        }

        // Binding succeeded without encryption issues; simply save the result
        mend.Save(outputPath);
        Console.WriteLine($"PDF bound and saved to '{outputPath}'.");
    }
}