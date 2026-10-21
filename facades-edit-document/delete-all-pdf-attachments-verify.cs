using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Work on a copy to keep the original file unchanged
        File.Copy(inputPath, outputPath, true);

        // Load the PDF. Attachments are accessed via the EmbeddedFiles collection.
        Document pdfDoc = new Document(outputPath);

        // Delete all embedded files (attachments) if any exist
        if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
        {
            // EmbeddedFileCollection uses 1‑based indexing. Iterate backwards and delete by name.
            for (int i = pdfDoc.EmbeddedFiles.Count; i >= 1; i--)
            {
                var fileSpec = pdfDoc.EmbeddedFiles[i];
                if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                {
                    pdfDoc.EmbeddedFiles.Delete(fileSpec.Name);
                }
            }

            // Save the document after removals
            pdfDoc.Save(outputPath);
        }

        // Verify that no attachments remain
        Document verifyDoc = new Document(outputPath);
        int remaining = verifyDoc.EmbeddedFiles != null ? verifyDoc.EmbeddedFiles.Count : 0;
        if (remaining == 0)
        {
            Console.WriteLine("All attachments deleted successfully. Count = 0.");
        }
        else
        {
            Console.WriteLine($"Deletion failed. Remaining attachments: {remaining}");
        }
    }
}