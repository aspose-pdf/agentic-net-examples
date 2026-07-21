using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "owner123"; // leave empty if unknown

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Document doc = null;

        // Attempt to open the PDF without a password.
        try
        {
            doc = new Document(inputPath);
        }
        catch (InvalidPasswordException)
        {
            // PDF is encrypted – try opening with the supplied owner password.
            try
            {
                doc = new Document(inputPath, ownerPassword);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unable to open encrypted PDF: {ex.Message}");
                return;
            }
        }

        // Ensure the document is disposed properly.
        using (doc)
        {
            // Simple modification: add a text annotation on the first page.
            Page page = doc.Pages[1]; // 1‑based indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Modified by Aspose.Pdf",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true
            };

            page.Annotations.Add(annotation);

            // Save the (decrypted) PDF with the modifications.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}