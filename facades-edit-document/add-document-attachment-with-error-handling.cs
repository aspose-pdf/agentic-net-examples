using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "input.pdf";          // PDF to which the attachment will be added
        const string attachmentPath = "attachment.docx";    // File to attach
        const string attachmentName = "DocumentAttachment"; // Name shown in the PDF attachment list

        // Verify that the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Verify that the attachment file path is provided and the file exists
        if (string.IsNullOrWhiteSpace(attachmentPath) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found or path is invalid: {attachmentPath}");
            return;
        }

        try
        {
            // Load the PDF document. Document.EmbeddedFiles is the correct collection for attachments.
            using (Document pdfDoc = new Document(sourcePdfPath))
            {
                // Create a FileSpecification for the attachment.
                // The constructor argument is the name that will appear in the attachment list.
                var fileSpec = new FileSpecification(attachmentName);
                // Load the file contents into a memory stream and assign it to the specification.
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
                // Optionally set a description (shown in some PDF viewers).
                fileSpec.Description = Path.GetFileName(attachmentPath);

                // Add the file specification to the EmbeddedFiles collection.
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF, overwriting the original file.
                pdfDoc.Save(sourcePdfPath);
            }

            Console.WriteLine($"Attachment '{attachmentName}' added to '{sourcePdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., permission issues, corrupted PDF)
            Console.Error.WriteLine($"Error adding attachment: {ex.Message}");
        }
    }
}
