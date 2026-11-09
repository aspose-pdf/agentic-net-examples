using System;
using System.IO;
using Aspose.Pdf; // Document, EmbeddedFiles handling

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

        // Work on a copy so the original file remains unchanged
        File.Copy(inputPath, outputPath, true);

        // Load the PDF using the Document API (EmbeddedFiles is the correct collection for attachments)
        Document pdfDoc = new Document(outputPath);

        // Delete all embedded files. The Delete method expects the attachment name (string),
        // not an index. Collect the names first to avoid modifying the collection while iterating.
        var embeddedFiles = pdfDoc.EmbeddedFiles;
        string[] attachmentNames = new string[embeddedFiles.Count];
        for (int i = 0; i < embeddedFiles.Count; i++)
        {
            attachmentNames[i] = embeddedFiles[i].Name; // each item is an EmbeddedFile object
        }
        foreach (var name in attachmentNames)
        {
            embeddedFiles.Delete(name);
        }

        // Save the changes
        pdfDoc.Save(outputPath);

        // Verify that no embedded files remain
        Document verifyDoc = new Document(outputPath);
        if (verifyDoc.EmbeddedFiles.Count == 0)
        {
            Console.WriteLine("All attachments deleted successfully. Attachment count is zero.");
        }
        else
        {
            Console.WriteLine($"Error: {verifyDoc.EmbeddedFiles.Count} attachment(s) still present.");
        }
    }
}
