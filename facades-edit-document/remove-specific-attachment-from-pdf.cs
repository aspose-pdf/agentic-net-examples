using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentToRemove = "OldReport.pdf";

        // -----------------------------------------------------------------
        // Ensure the source PDF exists – create a simple one if it does not.
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            CreateSamplePdfWithAttachment(inputPdfPath, attachmentToRemove);
            Console.WriteLine($"Sample PDF created at '{inputPdfPath}'.");
        }

        // -----------------------------------------------------------------
        // Extract all existing attachments from the source PDF
        // -----------------------------------------------------------------
        IList<string> attachmentNames;
        MemoryStream[] attachmentStreams;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractAttachment();                     // extracts all attachments
            attachmentNames = extractor.GetAttachNames();      // names of attachments (generic IList)
            attachmentStreams = extractor.GetAttachment();    // data streams
        }

        // -----------------------------------------------------------------
        // Remove all attachments from the PDF using PdfContentEditor
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            editor.DeleteAttachments(); // clears every attachment

            // -----------------------------------------------------------------
            // Re‑add every attachment except the one we want to delete
            // -----------------------------------------------------------------
            for (int i = 0; i < attachmentNames.Count; i++)
            {
                string name = attachmentNames[i];
                if (!string.Equals(name, attachmentToRemove, StringComparison.OrdinalIgnoreCase))
                {
                    // Ensure the stream is positioned at the beginning before adding
                    MemoryStream ms = attachmentStreams[i];
                    ms.Position = 0;
                    // AddDocumentAttachment(Stream, string name, string description)
                    editor.AddDocumentAttachment(ms, name, "Re‑added attachment");
                }
            }

            // -----------------------------------------------------------------
            // Save the modified PDF without the unwanted attachment
            // -----------------------------------------------------------------
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment '{attachmentToRemove}' removed. Output saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Creates a minimal PDF containing a single page and embeds a dummy attachment.
    /// This helper is used only when the expected input file is missing.
    /// </summary>
    private static void CreateSamplePdfWithAttachment(string pdfPath, string attachmentFileName)
    {
        // Create a simple PDF document with one blank page.
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            // Add a dummy attachment (the content is just a short text).
            byte[] dummyContent = System.Text.Encoding.UTF8.GetBytes("This is a dummy attachment file.");
            using (MemoryStream ms = new MemoryStream(dummyContent))
            {
                // FileSpecification constructor (string fileName, string description)
                FileSpecification fileSpec = new FileSpecification(attachmentFileName, "Dummy attachment");
                fileSpec.Contents = ms;
                doc.EmbeddedFiles.Add(fileSpec);
            }
            doc.Save(pdfPath);
        }
    }
}
