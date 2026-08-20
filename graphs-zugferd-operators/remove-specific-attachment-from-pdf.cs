using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "example.txt"; // name of the attachment to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove the specific embedded file by its filename
            // EmbeddedFiles is an EmbeddedFileCollection which provides Delete(string)
            doc.EmbeddedFiles.Delete(attachmentName);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment '{attachmentName}' removed. Saved to '{outputPath}'.");
    }
}