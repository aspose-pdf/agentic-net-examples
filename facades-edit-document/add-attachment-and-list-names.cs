using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added namespace for TextFragment

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Prepare sample files (self‑contained – no external files required)
        // -----------------------------------------------------------------
        const string inputPdfPath = "input.pdf";
        const string attachmentPdfPath = "attachment_file.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Create a minimal source PDF if it does not exist
        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPdfPath);
            }
        }

        // Create a minimal attachment PDF if it does not exist
        if (!File.Exists(attachmentPdfPath))
        {
            using (var attDoc = new Document())
            {
                attDoc.Pages.Add();
                // Add a simple paragraph so the file is not completely empty
                attDoc.Pages[1].Paragraphs.Add(new TextFragment("Attachment PDF"));
                attDoc.Save(attachmentPdfPath);
            }
        }

        // -----------------------------------------------------------------
        // 2. Add the attachment to the source PDF (no visual annotation)
        // -----------------------------------------------------------------
        using (var editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            editor.AddDocumentAttachment(attachmentPdfPath, "Sample attachment");
            editor.Save(outputPdfPath);
        }

        // -----------------------------------------------------------------
        // 3. Extract and list attachment names to verify the addition
        // -----------------------------------------------------------------
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(outputPdfPath);
            extractor.ExtractAttachment(); // required before GetAttachNames()
            IList<string> attachmentNames = extractor.GetAttachNames();

            Console.WriteLine("Attachments in the PDF:");
            foreach (string name in attachmentNames)
            {
                Console.WriteLine("- " + name);
            }
        }
    }
}
