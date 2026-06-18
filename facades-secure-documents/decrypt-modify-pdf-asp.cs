using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempDecryptedPath = "temp_decrypted.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "ownerpass"; // set to empty if unknown

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine whether the PDF is encrypted by attempting to open it.
        bool isEncrypted = false;
        try
        {
            using (var testDoc = new Document(inputPath))
            {
                // Opened successfully – not encrypted.
            }
        }
        catch
        {
            isEncrypted = true;
        }

        string workingPath = inputPath;

        if (isEncrypted)
        {
            // Decrypt the file to a temporary location using PdfFileSecurity.
            var security = new PdfFileSecurity(inputPath, tempDecryptedPath);
            security.DecryptFile(ownerPassword);
            workingPath = tempDecryptedPath;
        }

        // Load (decrypted) document, modify it, and save the result.
        using (Document doc = new Document(workingPath))
        {
            // Example modification: add a text fragment to the first page.
            Page page = doc.Pages[1]; // 1‑based indexing
            TextFragment tf = new TextFragment("Modified by Aspose.Pdf");
            tf.Position = new Position(100, 700);
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            page.Paragraphs.Add(tf);

            // Save the modified document.
            doc.Save(outputPath);
        }

        // Remove temporary file if it was created.
        if (isEncrypted && File.Exists(tempDecryptedPath))
        {
            try { File.Delete(tempDecryptedPath); } catch { }
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}