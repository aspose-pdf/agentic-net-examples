using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, process, and save inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Flatten the document to remove all annotations (comments, markup) and form fields.
            // The parameter‑less Flatten method is available in all recent Aspose.Pdf versions.
            doc.Flatten();

            // Remove all embedded files (attachments) from the document, if any exist.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                doc.EmbeddedFiles.Delete();
            }

            // Optional: clean up metadata as well.
            doc.RemoveMetadata();

            // Save the cleaned PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
