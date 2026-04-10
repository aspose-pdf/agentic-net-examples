using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path where the PDF with the embedded attachment will be saved
        const string outputPdfPath = "output_with_attachment.pdf";

        // ------------------------------------------------------------
        // 1. Create a simple source PDF in memory (so we don't need an
        //    external "input.pdf" file). This satisfies the requirement
        //    of PdfContentEditor which can work with a Stream overload.
        // ------------------------------------------------------------
        using (MemoryStream sourcePdfStream = new MemoryStream())
        {
            // Create a minimal PDF document with one blank page.
            using (Document srcDoc = new Document())
            {
                srcDoc.Pages.Add();
                srcDoc.Save(sourcePdfStream);
            }

            // Reset the stream position before binding.
            sourcePdfStream.Position = 0;

            // --------------------------------------------------------
            // 2. Prepare the attachment data (also in a MemoryStream).
            // --------------------------------------------------------
            byte[] attachmentBytes = System.Text.Encoding.UTF8.GetBytes(
                "This is the content of the attachment.");
            using (MemoryStream attachmentStream = new MemoryStream(attachmentBytes))
            {
                // ----------------------------------------------------
                // 3. Use PdfContentEditor to bind the source PDF stream,
                //    add the attachment, and save the result.
                // ----------------------------------------------------
                PdfContentEditor editor = new PdfContentEditor();
                // BindPdf has an overload that accepts a Stream.
                editor.BindPdf(sourcePdfStream);

                // Add the attachment from the memory stream.
                // Parameters: stream, attachment name, description.
                editor.AddDocumentAttachment(
                    attachmentStream,
                    "attachment.txt",
                    "Sample attachment from memory stream");

                // Save the modified PDF to the output file.
                editor.Save(outputPdfPath);
            }
        }
    }
}
