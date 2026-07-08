using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for source PDF, attachment file and output PDFs
        const string inputPdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.bin";
        const string description = "Sample attachment";
        const string attachedPdfPath = "attached.pdf";
        const string finalPdfPath = "final.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // -------------------------------------------------
        // 1. Add the attachment to the PDF using PdfContentEditor
        // -------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);                                   // Load source PDF
            editor.AddDocumentAttachment(attachmentFilePath, description); // Add attachment without annotation
            editor.Save(attachedPdfPath);                                   // Save intermediate PDF with attachment
        }

        // -------------------------------------------------
        // 2. Open the PDF with the attachment, compute its checksum,
        //    and store the checksum in custom metadata
        // -------------------------------------------------
        using (Document doc = new Document(attachedPdfPath))
        {
            // Find the embedded file specification that matches the attachment name
            string attachmentFileName = Path.GetFileName(attachmentFilePath);
            string checksum = null;

            foreach (FileSpecification fs in doc.EmbeddedFiles)
            {
                // Use the correct property 'Name' to get the original file name
                if (fs.Name.Equals(attachmentFileName, StringComparison.OrdinalIgnoreCase))
                {
                    // Use the correct property 'Params' to access checksum information
                    checksum = fs.Params?.CheckSum;
                    break;
                }
            }

            if (checksum == null)
            {
                Console.Error.WriteLine("Attachment not found in the PDF or checksum unavailable.");
                return;
            }

            // Store the checksum as custom metadata using PdfFileInfo
            using (PdfFileInfo pdfInfo = new PdfFileInfo(doc))
            {
                pdfInfo.SetMetaInfo("AttachmentChecksum", checksum); // Custom key/value pair
                pdfInfo.SaveNewInfo(finalPdfPath);                  // Persist changes to a new file
            }

            Console.WriteLine($"Checksum '{checksum}' stored in metadata. Output saved to '{finalPdfPath}'.");
        }
    }
}
