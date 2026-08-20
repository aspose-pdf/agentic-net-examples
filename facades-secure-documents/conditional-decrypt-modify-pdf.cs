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
        const string outputPath = "output.pdf";
        const string ownerPassword = "owner123"; // password used only if the PDF is encrypted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Document doc = null;

        // Try to open the PDF without a password.
        // If it is encrypted an InvalidPasswordException will be thrown.
        try
        {
            doc = new Document(inputPath);
        }
        catch (InvalidPasswordException)
        {
            // PDF is encrypted – use PdfFileSecurity to decrypt it.
            // DecryptFile creates a new decrypted document which we save to a temporary file.
            string tempDecrypted = Path.GetTempFileName();

            using (var security = new PdfFileSecurity())
            {
                security.BindPdf(inputPath);
                bool decrypted = security.DecryptFile(ownerPassword);
                if (!decrypted)
                {
                    Console.Error.WriteLine("Failed to decrypt the PDF with the provided password.");
                    return;
                }

                security.Save(tempDecrypted);
            }

            // Load the decrypted temporary file.
            doc = new Document(tempDecrypted);

            // Clean up the temporary file.
            try { File.Delete(tempDecrypted); } catch { /* ignore */ }
        }

        // Ensure the Document is disposed properly.
        using (doc)
        {
            // Example modification: add a text fragment to the first page.
            Page page = doc.Pages[1];
            TextFragment tf = new TextFragment("Modified by Aspose.Pdf");
            tf.Position = new Position(100, 700);
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            page.Paragraphs.Add(tf);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}