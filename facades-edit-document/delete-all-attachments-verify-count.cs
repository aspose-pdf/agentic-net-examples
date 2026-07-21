using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_no_attachments.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Delete all attachments ----------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);                 // Load the source PDF
        editor.DeleteAttachments();               // Remove every attachment
        editor.Save(outputPdf);                   // Persist the changes

        // ---------- Verify that no attachments remain ----------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdf);             // Load the modified PDF
        extractor.ExtractAttachment();            // Required before querying attachment info

        // Get the list of attachments (if any)
        List<FileSpecification> attachmentInfo = extractor.GetAttachmentInfo();
        int attachmentCount = attachmentInfo?.Count ?? 0;

        Console.WriteLine($"Attachment count after deletion: {attachmentCount}");
        if (attachmentCount == 0)
        {
            Console.WriteLine("All attachments successfully removed.");
        }
        else
        {
            Console.WriteLine("Some attachments remain.");
        }
    }
}