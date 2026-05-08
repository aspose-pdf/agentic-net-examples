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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate the index against the collection count
            if (attachmentIndex < 0 || attachmentIndex >= doc.EmbeddedFiles.Count)
            {
                Console.Error.WriteLine("Invalid attachment index.");
                return;
            }

            // Get the embedded file at the specified zero‑based index
            var embeddedFile = doc.EmbeddedFiles[attachmentIndex];

            // EmbeddedFileCollection only provides Delete(string name), so delete by name
            doc.EmbeddedFiles.Delete(embeddedFile.Name);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment at index {attachmentIndex} removed. Saved to '{outputPath}'.");
    }
}