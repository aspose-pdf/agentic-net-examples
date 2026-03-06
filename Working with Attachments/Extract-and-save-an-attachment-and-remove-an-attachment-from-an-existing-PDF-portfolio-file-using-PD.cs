using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "portfolio.pdf";               // source PDF with attachments
        const string outputPdf = "portfolio_modified.pdf";      // PDF after attachments are removed
        const string extractedAttachmentPath = "extracted.bin"; // where the extracted file will be saved

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Extract the first attachment ----------
        using (Document doc = new Document(inputPdf))
        {
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;
            if (embeddedFiles != null && embeddedFiles.Count > 0)
            {
                // Iterate over the collection; extract the first one found
                foreach (FileSpecification spec in embeddedFiles)
                {
                    // spec.Contents is a Stream containing the attachment data
                    using (FileStream outStream = File.Create(extractedAttachmentPath))
                    {
                        spec.Contents.CopyTo(outStream);
                    }

                    Console.WriteLine($"Attachment '{spec.Name}' extracted to '{extractedAttachmentPath}'.");
                    break; // only extract one attachment as per requirement
                }
            }
            else
            {
                Console.WriteLine("No attachments found in the PDF.");
            }
        }

        // ---------- Remove all attachments and save the PDF ----------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);          // load the same PDF for editing
        editor.DeleteAttachments();        // delete every embedded file
        editor.Save(outputPdf);            // persist changes
        editor.Close();                    // release resources

        Console.WriteLine($"All attachments removed. Modified PDF saved as '{outputPdf}'.");
    }
}