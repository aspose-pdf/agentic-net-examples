using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, and the resulting PDF
        const string sourcePdfPath = "input.pdf";
        const string attachmentFilePath = "invoice2023.pdf";
        const string resultPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Bind the existing PDF document
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(sourcePdfPath);

        // Create a file attachment annotation on page 1.
        // The rectangle defines where the attachment icon will appear.
        // PdfContentEditor.CreateFileAttachment expects a System.Drawing.Rectangle.
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 20, 20); // small icon at top‑left
        editor.CreateFileAttachment(rect, "Invoice2023", attachmentFilePath, 1, "Graph");

        // Save the intermediate PDF (with the new annotation) to a temporary file
        string tempPath = Path.GetTempFileName();
        editor.Save(tempPath);

        // Load the temporary PDF to modify the annotation's creation date
        Document tempDoc = new Document(tempPath);
        Page page = tempDoc.Pages[1];

        // Annotations collection is 1‑based; get the last annotation added
        if (page.Annotations.Count > 0)
        {
            Annotation ann = page.Annotations[page.Annotations.Count];
            if (ann is FileAttachmentAnnotation fileAnn)
            {
                // Set the creation date to the current system time
                fileAnn.CreationDate = DateTime.Now;
            }
        }

        // Save the final PDF with the updated creation date
        tempDoc.Save(resultPdfPath);

        // Clean up the temporary file
        File.Delete(tempPath);
    }
}
