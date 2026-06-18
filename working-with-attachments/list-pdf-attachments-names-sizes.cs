using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            EmbeddedFileCollection attachments = doc.EmbeddedFiles;

            if (attachments == null || attachments.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            Console.WriteLine("Attachments found:");

            // EmbeddedFileCollection is 1‑based, iterate by index
            for (int i = 1; i <= attachments.Count; i++)
            {
                FileSpecification spec = attachments[i];
                string name = spec.Name ?? $"Attachment_{i}";
                long size = 0;

                // Preferred way: use Params.Size (int, not nullable)
                if (spec.Params != null)
                {
                    size = spec.Params.Size;
                }
                // Fallback: read the Contents stream to determine size
                else if (spec.Contents != null)
                {
                    // Ensure the stream is at the beginning
                    if (spec.Contents.CanSeek)
                        spec.Contents.Position = 0;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        spec.Contents.CopyTo(ms);
                        size = ms.Length;
                    }
                }

                Console.WriteLine($"- Name: {name}, Size: {size} bytes");
            }
        }
    }
}
