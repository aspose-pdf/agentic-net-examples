using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists; create a minimal placeholder if it does not.
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Load PDF bytes into a memory stream.
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (var memoryStream = new MemoryStream(pdfBytes))
        {
            // Load the document from the stream.
            using (var pdfDoc = new Document(memoryStream))
            {
                // Change the PDF version to 1.4 using Convert (Document.Version is read‑only).
                pdfDoc.Convert("conversion_log.xml", PdfFormat.v_1_4, ConvertErrorAction.Delete);

                // Save the modified PDF to the file system.
                pdfDoc.Save(outputPath);
            }
        }
    }
}
