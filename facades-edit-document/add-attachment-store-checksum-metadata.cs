using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "attachment.bin";
        const string attachmentDescription = "Attachment for integrity check";

        // Ensure files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Add the attachment to the PDF using PdfContentEditor (Facades API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

            // Access the underlying Document to retrieve the checksum of the added attachment
            Document doc = editor.Document;
            string attachmentFileName = Path.GetFileName(attachmentPath);
            string checksum = string.Empty;

            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Use the correct property "Name" to get the embedded file name
                if (fileSpec.Name.Equals(attachmentFileName, StringComparison.OrdinalIgnoreCase))
                {
                    // The checksum is available via the Params.CheckSum property (MD5 of the uncompressed bytes)
                    checksum = fileSpec.Params?.CheckSum ?? string.Empty;
                    break;
                }
            }

            // Store the checksum in custom metadata using PdfFileInfo
            using (PdfFileInfo info = new PdfFileInfo(doc))
            {
                // Custom key "AttachmentChecksum" holds the calculated checksum
                info.SetMetaInfo("AttachmentChecksum", checksum);
                // Save the updated PDF with the new metadata
                info.SaveNewInfo(outputPdfPath);
            }
        }

        Console.WriteLine($"Attachment added and checksum stored in metadata. Output saved to '{outputPdfPath}'.");
    }
}
