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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            bool anyAttachment = false;

            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Look for file attachment annotations
                    if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null)
                    {
                        anyAttachment = true;
                        // Use FileSpecification.Name to get the original file name
                        string name = fileAnn.File.Name;

                        // Determine size: first try Params.Size, otherwise fall back to the length of the Contents stream
                        long size = 0;
                        if (fileAnn.File.Params != null)
                        {
                            // Params.Size is an int, not nullable, so we can use it directly
                            size = (long)fileAnn.File.Params.Size;
                        }
                        else if (fileAnn.File.Contents != null)
                        {
                            try
                            {
                                size = fileAnn.File.Contents.Length;
                            }
                            catch (NotSupportedException)
                            {
                                // Stream does not support Length; read to memory to compute size
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    fileAnn.File.Contents.CopyTo(ms);
                                    size = ms.Length;
                                }
                            }
                        }

                        Console.WriteLine($"Attachment: {name}, Size: {size} bytes");
                    }
                }
            }

            if (!anyAttachment)
            {
                Console.WriteLine("No attachments found in the PDF.");
            }
        }
    }
}
