using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "output_with_attachment.pdf";
        const string attachmentPath = "sample.txt";

        // Ensure there is a file to attach
        if (!File.Exists(attachmentPath))
        {
            File.WriteAllText(attachmentPath, "Sample attachment content.");
        }

        // Create a simple PDF and add a Unicode‑named attachment
        using (Document doc = new Document())
        {
            // Add a blank page so the PDF is not empty
            doc.Pages.Add();

            // Create a file specification for the attachment
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
            // Set the Unicode filename (display name inside the PDF)
            fileSpec.Name = "示例附件.txt";
            // Load the file contents
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

            // Add the attachment to the document via the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF
            doc.Save(outputPdf);
        }

        // Verify that the attachment was added with the correct Unicode name
        using (Document verifyDoc = new Document(outputPdf))
        {
            if (verifyDoc.EmbeddedFiles != null && verifyDoc.EmbeddedFiles.Count > 0)
            {
                // Retrieve the first attachment (1‑based indexing)
                FileSpecification attachedFile = verifyDoc.EmbeddedFiles[1];
                Console.WriteLine($"Attachment filename: {attachedFile.Name}");
                // Expected output: 示例附件.txt
            }
            else
            {
                Console.WriteLine("No attachments found.");
            }
        }
    }
}
