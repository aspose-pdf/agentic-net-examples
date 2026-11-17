using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentToDelete = "example.txt"; // filename to match

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Embedded files are stored in the Document.EmbeddedFiles collection (1‑based index)
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // Iterate using 1‑based indexing as required by Aspose.PDF
                for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
                {
                    var fileSpec = doc.EmbeddedFiles[i];
                    if (fileSpec != null &&
                        string.Equals(fileSpec.Name, attachmentToDelete, StringComparison.OrdinalIgnoreCase))
                    {
                        // Delete by name – the collection provides a Delete(string name) overload
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                        Console.WriteLine($"Attachment '{attachmentToDelete}' removed.");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("No embedded files found in the PDF.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
    }
}
