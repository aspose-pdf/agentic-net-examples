using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AttachmentPerformance
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "sample.txt";
        const string extractedPath = "extracted_sample.txt";

        if (!File.Exists(inputPdf) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // -----------------------------------------------------------------
        // Add attachment and measure time
        // -----------------------------------------------------------------
        var swAdd = Stopwatch.StartNew();

        using (Document doc = new Document(inputPdf))
        {
            // Use first page for the annotation
            Page page = doc.Pages[1];

            // Define rectangle for the attachment icon
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a FileSpecification pointing to the file to attach
            FileSpecification fileSpec = new FileSpecification(attachmentPath);

            // Create the file attachment annotation
            FileAttachmentAnnotation attachAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional visual settings
                Icon = FileIcon.Paperclip, // Correct enum value
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Sample attachment"
            };

            // Add annotation to the page
            page.Annotations.Add(attachAnnot);

            // Save the document with the new attachment
            doc.Save(outputPdf);
        }

        swAdd.Stop();
        Console.WriteLine($"Add attachment time: {swAdd.ElapsedMilliseconds} ms");

        // -----------------------------------------------------------------
        // Extract attachment and measure time
        // -----------------------------------------------------------------
        var swExtract = Stopwatch.StartNew();

        using (Document doc = new Document(outputPdf))
        {
            foreach (Page page in doc.Pages)
            {
                // Iterate over all annotations on the page
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // Retrieve the attached file specification
                        FileSpecification spec = fileAnn.File;

                        // Save the attached file to disk using the Contents stream
                        using (FileStream outStream = File.Create(extractedPath))
                        using (Stream contentStream = spec.Contents)
                        {
                            contentStream.CopyTo(outStream);
                        }

                        Console.WriteLine($"Extracted attachment saved to '{extractedPath}'.");
                    }
                }
            }
        }

        swExtract.Stop();
        Console.WriteLine($"Extract attachment time: {swExtract.ElapsedMilliseconds} ms");

        // -----------------------------------------------------------------
        // Remove attachment and measure time
        // -----------------------------------------------------------------
        var swRemove = Stopwatch.StartNew();

        using (Document doc = new Document(outputPdf))
        {
            foreach (Page page in doc.Pages)
            {
                // Collect indices of file attachment annotations to delete
                var indicesToDelete = new List<int>();

                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    if (page.Annotations[i] is FileAttachmentAnnotation)
                    {
                        indicesToDelete.Add(i);
                    }
                }

                // Delete annotations in reverse order to keep indices valid
                for (int i = indicesToDelete.Count - 1; i >= 0; i--)
                {
                    page.Annotations.Delete(indicesToDelete[i]);
                }
            }

            // Save the document after removal
            doc.Save("output_no_attachments.pdf");
        }

        swRemove.Stop();
        Console.WriteLine($"Remove attachment time: {swRemove.ElapsedMilliseconds} ms");
    }
}
