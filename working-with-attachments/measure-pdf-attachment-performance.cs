using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class AttachmentPerformance
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdfAdd = "output_add.pdf";
        const string outputPdfRemove = "output_remove.pdf";
        const string attachmentFile = "sample.txt";
        const string extractFolder = "ExtractedAttachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the attachment file exists
        if (!File.Exists(attachmentFile))
        {
            File.WriteAllText(attachmentFile, "This is a sample attachment.");
        }

        // ---------------- Add attachment ----------------
        Stopwatch swAdd = Stopwatch.StartNew();
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileSpecification for the attachment
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentFile))
            {
                // Set the file contents via a MemoryStream
                Contents = new MemoryStream(File.ReadAllBytes(attachmentFile))
            };
            // Add to the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);
            doc.Save(outputPdfAdd);
        }
        swAdd.Stop();
        Console.WriteLine($"Add attachment time: {swAdd.ElapsedMilliseconds} ms");

        // ---------------- Extract attachments ----------------
        Stopwatch swExtract = Stopwatch.StartNew();
        Directory.CreateDirectory(extractFolder);
        using (Document doc = new Document(outputPdfAdd))
        {
            // Iterate using 1‑based indexing (the collection implements IEnumerable, but 1‑based is the official API contract)
            for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
            {
                FileSpecification fileSpec = doc.EmbeddedFiles[i];
                string outPath = Path.Combine(extractFolder, fileSpec.Name);
                // Ensure the stream is at the beginning before copying
                if (fileSpec.Contents.CanSeek)
                    fileSpec.Contents.Position = 0;
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    fileSpec.Contents.CopyTo(outStream);
                }
                Console.WriteLine($"Extracted: {fileSpec.Name}");
            }
        }
        swExtract.Stop();
        Console.WriteLine($"Extract attachments time: {swExtract.ElapsedMilliseconds} ms");

        // ---------------- Remove attachment ----------------
        Stopwatch swRemove = Stopwatch.StartNew();
        using (Document doc = new Document(outputPdfAdd))
        {
            // Delete each embedded file individually – EmbeddedFileCollection has no Clear() method.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    var fileSpec = doc.EmbeddedFiles[i];
                    if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                    {
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                    }
                }
            }
            doc.Save(outputPdfRemove);
        }
        swRemove.Stop();
        Console.WriteLine($"Remove attachment time: {swRemove.ElapsedMilliseconds} ms");
    }
}
