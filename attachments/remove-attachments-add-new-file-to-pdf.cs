using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newAttachmentPath = "newAttachment.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(newAttachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {newAttachmentPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Remove any existing embedded files (attachments)
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // EmbeddedFileCollection uses 1‑based indexing and provides Delete(string name)
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    var existingSpec = doc.EmbeddedFiles[i];
                    if (existingSpec != null && !string.IsNullOrEmpty(existingSpec.Name))
                    {
                        doc.EmbeddedFiles.Delete(existingSpec.Name);
                    }
                }
            }

            // Create a FileSpecification for the new attachment
            var fileSpec = new FileSpecification(Path.GetFileName(newAttachmentPath))
            {
                // Load the file data into a memory stream
                Contents = new MemoryStream(File.ReadAllBytes(newAttachmentPath))
            };

            // Add the new attachment to the PDF
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}
