using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentToRemove = "OldReport.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Remove the embedded file (attachment) if it exists
            bool removed = false;
            if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
            {
                // EmbeddedFileCollection uses 1‑based indexing
                for (int i = pdfDoc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    FileSpecification fileSpec = pdfDoc.EmbeddedFiles[i];
                    if (fileSpec != null &&
                        string.Equals(fileSpec.Name, attachmentToRemove, StringComparison.OrdinalIgnoreCase))
                    {
                        // Delete by name – there is no RemoveAt method
                        pdfDoc.EmbeddedFiles.Delete(fileSpec.Name);
                        removed = true;
                        break;
                    }
                }
            }

            Console.WriteLine(removed
                ? $"Attachment '{attachmentToRemove}' removed."
                : $"Attachment '{attachmentToRemove}' not found.");

            // Save the modified PDF to a new file
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
