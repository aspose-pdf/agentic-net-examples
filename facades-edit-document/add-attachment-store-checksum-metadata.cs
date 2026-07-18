using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.bin";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the PDF using the Facades API
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Add a document attachment (no visible annotation)
        editor.AddDocumentAttachment(attachmentPath, "Sample attachment");

        // Save the modified PDF into a memory stream for further processing
        using (MemoryStream ms = new MemoryStream())
        {
            editor.Save(ms);
            ms.Position = 0; // reset stream position for reading

            // Load the PDF as a Document to access attachment details
            using (Document doc = new Document(ms))
            {
                // Retrieve the checksum (MD5) of the first embedded file
                string checksum = string.Empty;
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    // EmbeddedFiles collection is 1‑based in Aspose.Pdf
                    var fileSpec = doc.EmbeddedFiles[1];
                    checksum = fileSpec.Params?.CheckSum ?? string.Empty;
                }

                // Store the checksum in custom metadata using PdfFileInfo
                PdfFileInfo info = new PdfFileInfo();
                info.BindPdf(doc);
                info.SetMetaInfo("AttachmentChecksum", checksum);
                info.SaveNewInfo(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved with attachment checksum metadata to '{outputPdf}'.");
    }
}
