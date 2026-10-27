using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int attachmentZeroIndex = 0; // zero‑based index of the attachment to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the PDF actually contains embedded files
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.Error.WriteLine("No embedded files found in the PDF.");
                return;
            }

            // Validate the supplied zero‑based index
            if (attachmentZeroIndex < 0 || attachmentZeroIndex >= doc.EmbeddedFiles.Count)
            {
                Console.Error.WriteLine("Invalid attachment index.");
                return;
            }

            // Aspose collections are 1‑based; convert the zero‑based index
            int aspIndex = attachmentZeroIndex + 1;

            // Retrieve the FileSpecification at the calculated index
            FileSpecification fileSpec = doc.EmbeddedFiles[aspIndex];

            // Delete the attachment by its name (Delete expects a string)
            doc.EmbeddedFiles.Delete(fileSpec.Name);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment at zero‑based index {attachmentZeroIndex} removed. Saved to '{outputPath}'.");
    }
}
