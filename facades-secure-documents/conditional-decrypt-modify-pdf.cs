using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "output.pdf";
        const string userPassword   = "user123";   // known user password
        const string ownerPassword  = "owner123";  // known owner password (optional)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Document doc = null;

        try
        {
            // Attempt to load without a password.
            doc = new Document(inputPath);
        }
        catch (InvalidPasswordException)
        {
            // The PDF is encrypted; load with the user password and decrypt.
            doc = new Document(inputPath, userPassword);
            doc.Decrypt(); // Decrypt takes no parameters.
        }

        // Ensure deterministic disposal of the Document.
        using (doc)
        {
            // Example modification: add a text fragment to the first page.
            Page page = doc.Pages[1];
            TextFragment tf = new TextFragment("Modified by Aspose.Pdf.Facades example");
            tf.TextState.FontSize = 12;
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Demonstrate usage of a Facades class (PdfFileEditor) – even if not required for this task.
            // Here we simply instantiate it; more complex file‑level operations could be added later.
            PdfFileEditor editor = new PdfFileEditor();

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}