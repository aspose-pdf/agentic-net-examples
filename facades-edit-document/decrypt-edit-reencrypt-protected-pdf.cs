using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths and passwords
        const string inputPath   = "protected_input.pdf";
        const string outputPath  = "protected_output.pdf";
        const string ownerPassword = "ownerPass"; // password that allows full permissions
        const string userPassword  = "userPass";  // password that will be required to open the final PDF

        // -------------------------------------------------
        // 1. Load the encrypted PDF (decryption is done automatically)
        // -------------------------------------------------
        // The Document constructor that accepts a password will decrypt the file.
        Document doc;
        try
        {
            doc = new Document(inputPath, ownerPassword);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to open encrypted PDF: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // 2. Edit the PDF (example: add a text annotation)
        // -------------------------------------------------
        if (doc.Pages.Count == 0)
        {
            Console.Error.WriteLine("Document contains no pages.");
            return;
        }

        Page page = doc.Pages[1]; // 1‑based indexing
        // Fully qualified rectangle to avoid ambiguity with System.Drawing
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        TextAnnotation annotation = new TextAnnotation(page, rect)
        {
            Title    = "Edit Note",
            Contents = "Added after decryption.",
            Color    = Aspose.Pdf.Color.Yellow,
            Open     = true,
            Icon     = TextIcon.Note
        };
        page.Annotations.Add(annotation);

        // -------------------------------------------------
        // 3. Re‑encrypt the edited PDF and save it
        // -------------------------------------------------
        // Define the permissions you want to grant to the user password.
        var permissions = Aspose.Pdf.Permissions.PrintDocument; // example: allow printing only
        // Encrypt using AES‑256 (the most secure algorithm currently supported).
        doc.Encrypt(userPassword, ownerPassword, permissions, Aspose.Pdf.CryptoAlgorithm.AESx256);

        try
        {
            doc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save encrypted PDF: {ex.Message}");
            return;
        }

        Console.WriteLine($"Decrypted, edited, and re‑encrypted PDF saved to '{outputPath}'.");
    }
}
