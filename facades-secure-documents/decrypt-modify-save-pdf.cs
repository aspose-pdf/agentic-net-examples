using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "owner123"; // adjust if known

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Attempt to open the PDF without a password.
            using (Document doc = new Document(inputPath))
            {
                ModifyDocument(doc);
                doc.Save(outputPath); // Save as PDF (no SaveOptions needed)
            }
        }
        catch (InvalidPasswordException)
        {
            // PDF is encrypted – use PdfFileSecurity to decrypt it.
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Initialize the security facade and bind the encrypted file.
            PdfFileSecurity security = new PdfFileSecurity();
            security.BindPdf(inputPath);

            // Decrypt using the owner password (or user password if owner unknown).
            security.DecryptFile(ownerPassword);

            // Save the decrypted intermediate PDF.
            security.Save(tempPath);

            // Load the decrypted PDF, modify, and save final output.
            using (Document doc = new Document(tempPath))
            {
                ModifyDocument(doc);
                doc.Save(outputPath);
            }

            // Clean up the temporary decrypted file.
            try { File.Delete(tempPath); } catch { }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Simple modification: add a text fragment to the first page.
    static void ModifyDocument(Document doc)
    {
        TextFragment tf = new TextFragment("Modified by Aspose.Pdf");
        tf.TextState.FontSize = 12;
        tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

        // Pages are 1‑based; add the fragment to the first page.
        doc.Pages[1].Paragraphs.Add(tf);
    }
}