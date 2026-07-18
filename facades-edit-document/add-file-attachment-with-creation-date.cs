using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF
        const string attachmentFile = "invoice2023.pdf";    // file to attach
        const string description = "Invoice2023";          // attachment description
        const string tempPdf = "temp_with_attachment.pdf"; // intermediate file
        const string outputPdf = "output.pdf";             // final result

        // ------------------------------------------------------------
        // 1. Create a minimal source PDF (input.pdf) for the demo.
        // ------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(inputPdf);
        }

        // ------------------------------------------------------------
        // 2. Create a dummy attachment file (invoice2023.pdf).
        // ------------------------------------------------------------
        using (Document attachDoc = new Document())
        {
            attachDoc.Pages.Add();
            attachDoc.Save(attachmentFile);
        }

        // ---------- Add the attachment using the Facades API ----------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Define a small rectangle where the attachment icon will appear (page 1)
        // The CreateFileAttachment overload expects a System.Drawing.Rectangle.
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 20, 20);

        // Create a file‑attachment annotation; the description is stored in the annotation's Contents
        editor.CreateFileAttachment(rect, description, attachmentFile, 1, "Paperclip");

        // Save the PDF that now contains the attachment annotation
        editor.Save(tempPdf);
        editor.Close();

        // ---------- Set the creation date on the attachment annotation ----------
        using (Document doc = new Document(tempPdf))
        {
            // The annotation was added to page 1
            Page page = doc.Pages[1];

            foreach (Annotation ann in page.Annotations)
            {
                if (ann is FileAttachmentAnnotation fileAnn)
                {
                    // Set the creation date to the current system time
                    fileAnn.CreationDate = DateTime.Now;

                    // Ensure the description is present (optional, already set via Contents)
                    fileAnn.Contents = description;
                }
            }

            // Save the final PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added with description '{description}' and current creation date. Output saved to '{outputPdf}'.");
    }
}
