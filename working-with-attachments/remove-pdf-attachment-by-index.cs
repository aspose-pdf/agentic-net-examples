using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int attachmentIndex = 0; // zero‑based index of the attachment to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify the index is valid and the document contains embedded files
                if (doc.EmbeddedFiles != null && attachmentIndex >= 0 && attachmentIndex < doc.EmbeddedFiles.Count)
                {
                    // Convert zero‑based index to Aspose.Pdf's 1‑based indexing
                    int oneBasedIndex = attachmentIndex + 1;

                    // Retrieve the file specification at the calculated position
                    FileSpecification fileSpec = doc.EmbeddedFiles[oneBasedIndex];

                    // Remove the attachment by its name (the Delete method works with the name)
                    if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                    {
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Attachment index {attachmentIndex} is out of range (0‑{(doc.EmbeddedFiles?.Count ?? 0) - 1}).");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment removed and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
