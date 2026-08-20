using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AttachmentPerformanceDemo
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentFilePath = "sample.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // -------------------------------------------------
            // Add a file attachment annotation and measure time
            // -------------------------------------------------
            Stopwatch swAdd = Stopwatch.StartNew();

            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentFilePath);

            // Define the rectangle where the annotation will appear (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create the FileAttachmentAnnotation on the first page (1‑based indexing)
            FileAttachmentAnnotation attachAnnot = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
            {
                // Optional visual properties (Icon enum is not available in this version, so it is omitted)
                Color = Aspose.Pdf.Color.Blue,
                Contents = $"Attachment: {Path.GetFileName(attachmentFilePath)}"
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(attachAnnot);

            swAdd.Stop();
            Console.WriteLine($"Add attachment time: {swAdd.ElapsedMilliseconds} ms");

            // -------------------------------------------------
            // Extract (list) attachments and measure time
            // -------------------------------------------------
            Stopwatch swExtract = Stopwatch.StartNew();

            // Iterate through all pages and collect file attachment annotations
            foreach (Page page in doc.Pages)
            {
                for (int i = 1; i <= page.Annotations.Count; i++) // 1‑based indexing
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null)
                    {
                        Console.WriteLine($"Found attachment on page {page.Number}: {fileAnn.File.Name}");
                    }
                }
            }

            swExtract.Stop();
            Console.WriteLine($"Extract attachments time: {swExtract.ElapsedMilliseconds} ms");

            // -------------------------------------------------
            // Remove the previously added attachment and measure time
            // -------------------------------------------------
            Stopwatch swRemove = Stopwatch.StartNew();

            // Find and remove the attachment annotation we added
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];
                for (int annIdx = page.Annotations.Count; annIdx >= 1; annIdx--) // iterate backwards when removing
                {
                    Annotation ann = page.Annotations[annIdx];
                    if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null &&
                        fileAnn.File.Name.Equals(Path.GetFileName(attachmentFilePath), StringComparison.OrdinalIgnoreCase))
                    {
                        page.Annotations.Delete(annIdx);
                    }
                }
            }

            swRemove.Stop();
            Console.WriteLine($"Remove attachment time: {swRemove.ElapsedMilliseconds} ms");

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdfPath}'.");
    }
}
