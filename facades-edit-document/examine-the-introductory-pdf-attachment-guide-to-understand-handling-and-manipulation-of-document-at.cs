using System;
using System.IO;
using System.Collections.Generic; // For IList<T>
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfAttachmentDemo
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf = "sample.pdf";                     // PDF containing attachments
        const string attachmentToAdd = "newAttachment.txt";       // File to attach
        const string outputWithAttachment = "sample_with_att.pdf";
        const string outputWithoutAttachment = "sample_no_att.pdf";
        const string extractFolder = "ExtractedAttachments";

        // ------------------------------------------------------------
        // Ensure the source PDF exists – create a minimal one if missing
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            var placeholder = new Document();
            placeholder.Pages.Add(); // add a blank page
            placeholder.Save(inputPdf);
            Console.WriteLine($"Created placeholder PDF: {inputPdf}");
        }

        // Ensure the extraction folder exists
        Directory.CreateDirectory(extractFolder);

        // ------------------------------------------------------------
        // 1. Extract existing attachments from the PDF
        // ------------------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);               // Load the source PDF
        extractor.ExtractAttachment();             // Prepare extraction

        // Get attachment names (generic IList<string>)
        IList<string> attachNames = extractor.GetAttachNames();

        // Get attachment streams
        MemoryStream[] attachStreams = extractor.GetAttachment();

        // Guard against PDFs that have no attachments (both collections can be null)
        if (attachNames != null && attachStreams != null && attachNames.Count == attachStreams.Length)
        {
            for (int i = 0; i < attachStreams.Length; i++)
            {
                string name = attachNames[i];
                string outPath = Path.Combine(extractFolder, name);

                // Write each attachment to a file
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    attachStreams[i].Position = 0;
                    attachStreams[i].CopyTo(fs);
                }
            }
        }
        else
        {
            Console.WriteLine("No attachments found in the source PDF.");
        }

        // ------------------------------------------------------------
        // 2. Add a new attachment to the PDF (no annotation)
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);                                 // Load original PDF
            editor.AddDocumentAttachment(attachmentToAdd, "Demo attachment");
            editor.Save(outputWithAttachment);                        // Save with new attachment
        }

        // ------------------------------------------------------------
        // 3. Delete all attachments from the PDF
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(outputWithAttachment);                     // Load PDF that has attachments
            editor.DeleteAttachments();                               // Remove all attachments
            editor.Save(outputWithoutAttachment);                     // Save cleaned PDF
        }

        Console.WriteLine("Attachment extraction, addition, and deletion completed.");
    }
}
