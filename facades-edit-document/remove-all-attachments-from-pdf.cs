using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Remove all embedded files (attachments) if any exist
            if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
            {
                // Iterate backwards because Delete modifies the collection size
                for (int i = pdfDoc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    var fileSpec = pdfDoc.EmbeddedFiles[i]; // 1‑based indexing
                    if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                    {
                        pdfDoc.EmbeddedFiles.Delete(fileSpec.Name);
                    }
                }
            }

            // Save the modified PDF to a new file
            pdfDoc.Save(outputPath);

            Console.WriteLine($"All attachments removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
