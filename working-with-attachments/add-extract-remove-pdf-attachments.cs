using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentFilePath = "sample.txt";

        if (!File.Exists(inputPdfPath) || !File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // ---------- Add attachment ----------
        Stopwatch swAdd = Stopwatch.StartNew();
        using (Document doc = new Document(inputPdfPath))
        {
            // Use first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a file specification for the attachment (description is optional)
            FileSpecification fileSpec = new FileSpecification(attachmentFilePath);

            // Define the annotation rectangle (position and size)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the file attachment annotation
            FileAttachmentAnnotation attachmentAnn = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                Contents = "Sample attachment"
            };

            // Add the annotation to the page
            page.Annotations.Add(attachmentAnn);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }
        swAdd.Stop();
        Console.WriteLine($"Add attachment time: {swAdd.ElapsedMilliseconds} ms");

        // ---------- Extract attachment ----------
        Stopwatch swExtract = Stopwatch.StartNew();
        using (Document doc = new Document(outputPdfPath))
        {
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // Retrieve the original file name via the Name property
                        string fileName = fileAnn.File.Name;

                        // Save the attached file to a temporary location using the Contents stream
                        string outPath = Path.Combine(Path.GetTempPath(), fileName);
                        using (FileStream outStream = File.Create(outPath))
                        {
                            fileAnn.File.Contents.CopyTo(outStream);
                        }
                        Console.WriteLine($"Extracted attachment to: {outPath}");
                    }
                }
            }
        }
        swExtract.Stop();
        Console.WriteLine($"Extract attachment time: {swExtract.ElapsedMilliseconds} ms");

        // ---------- Remove attachment ----------
        Stopwatch swRemove = Stopwatch.StartNew();
        using (Document doc = new Document(outputPdfPath))
        {
            foreach (Page page in doc.Pages)
            {
                // Collect annotation indices to delete (cannot modify collection while iterating)
                var indicesToDelete = new List<int>();
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    if (page.Annotations[i] is FileAttachmentAnnotation)
                    {
                        indicesToDelete.Add(i);
                    }
                }

                // Delete in reverse order to keep indices valid
                for (int i = indicesToDelete.Count - 1; i >= 0; i--)
                {
                    page.Annotations.Delete(indicesToDelete[i]);
                }
            }

            // Save the cleaned PDF
            doc.Save("clean.pdf");
        }
        swRemove.Stop();
        Console.WriteLine($"Remove attachment time: {swRemove.ElapsedMilliseconds} ms");
    }
}
