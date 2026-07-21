using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string fileUrl   = "https://example.com/sample.txt"; // remote file URL

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // ---- download remote file into memory ----
        byte[] fileBytes;
        string fileName;
        using (HttpClient client = new HttpClient())
        {
            // extract file name from URL
            fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);
            // synchronous download (Result blocks until complete)
            fileBytes = client.GetByteArrayAsync(fileUrl).Result;
        }

        // ---- open PDF, add attachment, and save ----
        using (Document doc = new Document(inputPdf))               // document‑disposal‑with‑using
        {
            // pages are 1‑based
            Page page = doc.Pages[1];

            // rectangle for the annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // create a FileSpecification from the in‑memory stream
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                FileSpecification fileSpec = new FileSpecification(ms, fileName);

                // create the file‑attachment annotation
                FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    Icon     = FileIcon.Paperclip, // corrected enum
                    Contents = $"Attached file: {fileName}",
                    Title    = "Remote File"
                };

                // add annotation to the page
                page.Annotations.Add(fileAnnot);
            }

            // save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Remote file attached and saved to '{outputPdf}'.");
    }
}
