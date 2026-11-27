using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "attachmentRemoved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the high‑level Document API.
        Document pdf = new Document(inputPath);

        // Remove all embedded file attachments.
        // EmbeddedFileCollection uses 1‑based indexing and does not provide a Clear() method.
        // Iterate backwards and delete each attachment by its name.
        if (pdf.EmbeddedFiles != null && pdf.EmbeddedFiles.Count > 0)
        {
            for (int i = pdf.EmbeddedFiles.Count; i >= 1; i--)
            {
                FileSpecification fileSpec = pdf.EmbeddedFiles[i];
                if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                {
                    pdf.EmbeddedFiles.Delete(fileSpec.Name);
                }
            }
        }

        // Save the modified PDF.
        pdf.Save(outputPath);

        Console.WriteLine($"All attachments removed. Saved to '{outputPath}'.");
    }
}
