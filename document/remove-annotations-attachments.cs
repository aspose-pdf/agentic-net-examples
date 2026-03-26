using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Remove annotations (comments) by flattening the document.
            // Flatten() removes annotations, form fields, links, etc.
            doc.Flatten();

            // Remove all embedded files (attachments) if any are present.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // EmbeddedFileCollection uses 1‑based indexing.
                // It does not provide a Clear() method; delete each entry by name.
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    FileSpecification fileSpec = doc.EmbeddedFiles[i];
                    if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                    {
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                    }
                }
            }

            // Save the cleaned PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations and attachments removed. Saved to '{outputPath}'.");
    }
}
