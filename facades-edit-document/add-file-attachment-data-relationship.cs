using System;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string attachment = "datafile.bin";
        const string outputPdf  = "output_with_attachment.pdf";

        // Ensure files exist
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Edit the PDF using the Facades API
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Create a file attachment annotation on page 1
        // System.Drawing.Rectangle defines the icon location on the page (x, y, width, height)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 700, 20, 20);
        editor.CreateFileAttachment(rect, "Data attachment", attachment, 1, "Paperclip");

        // Retrieve the newly added annotation to set its relationship type
        // The annotation is added to the page's Annotations collection; it will be the last one.
        Page page = editor.Document.Pages[1];
        if (page.Annotations.Count > 0)
        {
            // Annotations collection is 1‑based
            Annotation lastAnn = page.Annotations[page.Annotations.Count];
            if (lastAnn is FileAttachmentAnnotation fileAnn)
            {
                // Set the AFRelationship to Data for better organization
                fileAnn.File.AFRelationship = Aspose.Pdf.AFRelationship.Data;
            }
        }

        // Save the modified PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}
