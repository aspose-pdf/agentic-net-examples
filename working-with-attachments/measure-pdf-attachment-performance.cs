using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "sample.txt";
        const string addedPdf = "added_attachment.pdf";
        const string extractedFolder = "extracted";
        const string removedPdf = "removed_attachment.pdf";

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

        // Ensure output folder exists
        Directory.CreateDirectory(extractedFolder);

        // -------------------------------------------------
        // Add attachment and measure time
        // -------------------------------------------------
        var swAdd = Stopwatch.StartNew();
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileSpecification for the file to embed
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentFile));
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFile));
            // Add the specification to the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF with the new embedded file
            doc.Save(addedPdf);
        }
        swAdd.Stop();
        Console.WriteLine($"Add attachment time: {swAdd.ElapsedMilliseconds} ms");

        // -------------------------------------------------
        // Extract attachment and measure time
        // -------------------------------------------------
        var swExtract = Stopwatch.StartNew();
        using (Document doc = new Document(addedPdf))
        {
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // EmbeddedFileCollection uses 1‑based indexing
                for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
                {
                    FileSpecification fs = doc.EmbeddedFiles[i];
                    string outputPath = Path.Combine(extractedFolder, fs.Name);
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        // Ensure the stream is at the beginning
                        if (fs.Contents.CanSeek)
                            fs.Contents.Position = 0;
                        fs.Contents.CopyTo(outStream);
                    }
                }
            }
        }
        swExtract.Stop();
        Console.WriteLine($"Extract attachment time: {swExtract.ElapsedMilliseconds} ms");

        // -------------------------------------------------
        // Remove attachment and measure time
        // -------------------------------------------------
        var swRemove = Stopwatch.StartNew();
        using (Document doc = new Document(addedPdf))
        {
            // Remove all embedded files (if any)
            while (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // Retrieve the first embedded file (1‑based index)
                FileSpecification firstSpec = doc.EmbeddedFiles[1];
                // Delete by name – Delete expects a string, not an index
                doc.EmbeddedFiles.Delete(firstSpec.Name);
            }

            // Save the PDF without embedded files
            doc.Save(removedPdf);
        }
        swRemove.Stop();
        Console.WriteLine($"Remove attachment time: {swRemove.ElapsedMilliseconds} ms");
    }
}
