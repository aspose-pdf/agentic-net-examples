using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AttachmentPerformanceDemo
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputPdf = "sample_with_attachments.pdf";
        const string attachmentFile = "example.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // ---------- Add attachment ----------
        var swAdd = Stopwatch.StartNew();
        using (Document doc = new Document(inputPdf))
        {
            // Create a file specification for the attachment
            var fileSpec = new FileSpecification(attachmentFile);

            // Define the rectangle where the annotation will appear (coordinates in points)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create the file attachment annotation on the first page
            var attachment = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
            {
                // Use the FileIcon enum instead of a string
                Icon = FileIcon.PushPin,
                Contents = "Attached example.txt",
                Color = Aspose.Pdf.Color.Blue
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(attachment);

            // Save the modified document
            doc.Save(outputPdf);
        }
        swAdd.Stop();
        Console.WriteLine($"Add attachment time: {swAdd.ElapsedMilliseconds} ms");

        // ---------- Extract attachment ----------
        var swExtract = Stopwatch.StartNew();
        using (Document doc = new Document(outputPdf))
        {
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // The FileSpecification contains the original file name
                        string attachedFileName = fileAnn.File.Name;
                        Console.WriteLine($"Found attachment: {attachedFileName}");

                        // Save the attached file to disk
                        using (Stream src = fileAnn.File.Contents)
                        using (FileStream dst = File.Create($"extracted_{attachedFileName}"))
                        {
                            src.CopyTo(dst);
                        }
                    }
                }
            }
        }
        swExtract.Stop();
        Console.WriteLine($"Extract attachment time: {swExtract.ElapsedMilliseconds} ms");

        // ---------- Remove attachment ----------
        var swRemove = Stopwatch.StartNew();
        using (Document doc = new Document(outputPdf))
        {
            foreach (Page page in doc.Pages)
            {
                // Iterate backwards when removing items to keep indexes valid
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is FileAttachmentAnnotation)
                    {
                        page.Annotations.Delete(i);
                    }
                }
            }

            // Save the document without the attachments
            doc.Save("sample_without_attachments.pdf");
        }
        swRemove.Stop();
        Console.WriteLine($"Remove attachment time: {swRemove.ElapsedMilliseconds} ms");
    }
}
