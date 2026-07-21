using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

        // -------------------------------------------------------------------
        // Ensure a source PDF exists. In the sandbox there is no pre‑existing file,
        // so we create a minimal PDF on‑the‑fly before editing it.
        // -------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document seed = new Document())
            {
                // Add a single blank page – enough for the attachment demo.
                seed.Pages.Add();
                seed.Save(inputPdfPath);
            }
        }

        // Create the attachment content in memory.
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("Sample attachment content");
        using (MemoryStream attachmentStream = new MemoryStream(attachmentData))
        {
            // Initialise the PdfContentEditor facade.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            // Attach the in‑memory stream to the PDF.
            // Parameters: (Stream attachment, string attachmentName, string description)
            editor.AddDocumentAttachment(attachmentStream, "sample.txt", "Sample text attachment");

            // Save the modified PDF.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}