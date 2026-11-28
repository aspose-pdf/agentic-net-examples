using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace provides Document and attachment handling

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedAttachments";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document using Aspose.Pdf.Document (the class that exposes attachments)
            var pdfDocument = new Document(inputPdf);

            // The collection that holds embedded files (attachments) – name may be EmbeddedFiles or Attachments
            // Both properties exist in recent versions; we handle whichever is available via reflection.
            var attachmentCollection = GetAttachmentCollection(pdfDocument);

            if (attachmentCollection == null || attachmentCollection.Count == 0)
            {
                Console.WriteLine("No attachments were found in the PDF.");
                return;
            }

            // Iterate over each attachment and write it to disk preserving the original file name and extension
            foreach (var attachment in attachmentCollection)
            {
                // "Name" contains the original file name with extension
                string originalName = attachment.Name;
                string outputPath = Path.Combine(outputFolder, originalName);

                // Write the raw bytes of the attachment to the file system
                File.WriteAllBytes(outputPath, attachment.Content);
            }

            Console.WriteLine($"All attachments have been extracted to '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }

    // Helper that abstracts the difference between Aspose.Pdf versions.
    // Returns a collection that implements IEnumerable<EmbeddedFile> (or Attachment) and has a Count property.
    private static dynamic GetAttachmentCollection(Document doc)
    {
        // Newer versions expose EmbeddedFiles property
        var type = doc.GetType();
        var embeddedProp = type.GetProperty("EmbeddedFiles");
        if (embeddedProp != null)
        {
            return embeddedProp.GetValue(doc, null);
        }
        // Older versions expose Attachments property
        var attachProp = type.GetProperty("Attachments");
        if (attachProp != null)
        {
            return attachProp.GetValue(doc, null);
        }
        return null; // No attachment collection found
    }
}
