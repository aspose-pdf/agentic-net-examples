using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection also uses 1‑based indexing
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // We're interested only in file attachment annotations
                    if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null)
                    {
                        // FileSpecification provides the attachment name
                        string name = fileAnn.File.Name ?? "(unnamed)";

                        // Determine size – Params.Size is an int (non‑nullable)
                        long size = 0;
                        if (fileAnn.File.Params != null)
                        {
                            size = fileAnn.File.Params.Size; // direct int value
                        }
                        else if (fileAnn.File.Contents != null)
                        {
                            // Contents is a Stream; copy to a MemoryStream to get its length.
                            using (var ms = new MemoryStream())
                            {
                                fileAnn.File.Contents.CopyTo(ms);
                                size = ms.Length;
                            }
                        }

                        Console.WriteLine($"Attachment: {name}, Size: {size} bytes");
                    }
                }
            }
        }
    }
}
