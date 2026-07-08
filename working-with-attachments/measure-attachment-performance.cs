using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentFile = "sample.txt"; // file to attach
        const string extractedDir = "extracted_attachments";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        Directory.CreateDirectory(extractedDir);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -------------------- Add Attachment --------------------
            Stopwatch swAdd = Stopwatch.StartNew();

            // Create a file specification for the attachment (description is optional)
            FileSpecification fileSpec = new FileSpecification(attachmentFile);

            // Define the rectangle where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Add the attachment annotation to the first page
            Page firstPage = doc.Pages[1];
            FileAttachmentAnnotation attachAnnot = new FileAttachmentAnnotation(firstPage, rect, fileSpec)
            {
                // Use the FileIcon enum instead of a string
                Icon = FileIcon.Paperclip,
                Contents = $"Attachment: {Path.GetFileName(attachmentFile)}"
            };
            firstPage.Annotations.Add(attachAnnot);

            swAdd.Stop();
            Console.WriteLine($"Add attachment time: {swAdd.ElapsedMilliseconds} ms");

            // -------------------- Extract Attachments --------------------
            Stopwatch swExtract = Stopwatch.StartNew();

            int extractedCount = 0;
            foreach (Page page in doc.Pages)
            {
                // Annotations collection is 1‑based
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null)
                    {
                        // Use the Name property of FileSpecification for the original file name
                        string outPath = Path.Combine(extractedDir, fileAnn.File.Name);
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            // Copy the embedded file's contents to the output file
                            fileAnn.File.Contents.CopyTo(fs);
                        }
                        extractedCount++;
                    }
                }
            }

            swExtract.Stop();
            Console.WriteLine($"Extract {extractedCount} attachment(s) time: {swExtract.ElapsedMilliseconds} ms");

            // -------------------- Remove Attachments --------------------
            Stopwatch swRemove = Stopwatch.StartNew();

            // Iterate backwards so we can delete safely while iterating
            foreach (Page page in doc.Pages)
            {
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is FileAttachmentAnnotation)
                    {
                        page.Annotations.Delete(i);
                    }
                }
            }

            swRemove.Stop();
            Console.WriteLine($"Remove attachment(s) time: {swRemove.ElapsedMilliseconds} ms");

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
    }
}
