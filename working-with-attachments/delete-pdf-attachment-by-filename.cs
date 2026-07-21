using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF containing attachments
        const string outputPath = "output.pdf";         // PDF after deletion
        const string attachmentName = "example.txt";    // Name of the embedded file to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete the embedded file by its filename (case‑sensitive)
            // EmbeddedFiles is an EmbeddedFileCollection; Delete(string) removes by name.
            doc.EmbeddedFiles.Delete(attachmentName);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment \"{attachmentName}\" removed. Saved to \"{outputPath}\".");
    }
}