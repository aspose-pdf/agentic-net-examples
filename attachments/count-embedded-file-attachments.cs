using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains FileAttachmentAnnotation objects
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int attachmentCount = 0;

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Enumerate annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Count only FileAttachmentAnnotation instances
                    if (annotation is FileAttachmentAnnotation)
                    {
                        attachmentCount++;
                    }
                }
            }

            Console.WriteLine($"Total embedded file attachments: {attachmentCount}");
        }
    }
}