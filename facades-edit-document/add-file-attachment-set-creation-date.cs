using System;
using System.Drawing; // needed for System.Drawing.Rectangle used by PdfContentEditor
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string attachmentPath = "Invoice2023.pdf"; // file to attach
        const string intermediatePath = "temp_with_attachment.pdf";
        const string outputPdfPath = "output.pdf";

        // Description for the attachment
        const string description = "Invoice2023";

        // ------------------------------------------------------------
        // 1. Ensure the source PDF exists – create a minimal one if missing.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPdfPath);
            }
        }

        // ------------------------------------------------------------
        // 2. Use PdfContentEditor (Facades) to embed the attachment.
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Create a small invisible rectangle on page 1 where the annotation will live.
            // Fully qualify the Rectangle type to avoid ambiguity with Aspose.Pdf.Rectangle.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 20, 20);

            // Create the file‑attachment annotation on page 1.
            // The 'name' parameter selects the icon style; "Paperclip" is a common choice.
            editor.CreateFileAttachment(rect, description, attachmentPath, 1, "Paperclip");

            // Save the PDF that now contains the attachment annotation.
            editor.Save(intermediatePath);
        }

        // ------------------------------------------------------------
        // 3. Open the resulting PDF with the core API to set CreationDate.
        // ------------------------------------------------------------
        using (Document doc = new Document(intermediatePath))
        {
            // Locate the FileAttachmentAnnotation we just added (page 1).
            Page page = doc.Pages[1];
            FileAttachmentAnnotation? fileAnn = null;

            foreach (Annotation ann in page.Annotations)
            {
                if (ann is FileAttachmentAnnotation faa)
                {
                    fileAnn = faa;
                    break; // assume the first one is ours
                }
            }

            if (fileAnn != null)
            {
                // Set the creation date to the current system time.
                fileAnn.CreationDate = DateTime.Now;
            }
            else
            {
                Console.Error.WriteLine("FileAttachmentAnnotation not found.");
            }

            // Save the final PDF with the updated creation date.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and creation date set. Output saved to '{outputPdfPath}'.");
    }
}
