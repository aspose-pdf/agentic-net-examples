using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class AttachmentPerformanceDemo
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachments.pdf";
        const string extractDir = "ExtractedAttachments";
        const string attachmentFile = "sample.txt";

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

        // Ensure the extraction directory exists
        Directory.CreateDirectory(extractDir);

        // -----------------------------------------------------------------
        // 1. Add attachment and measure time
        // -----------------------------------------------------------------
        var swAdd = Stopwatch.StartNew();

        using (Document doc = new Document(inputPdf))
        {
            // Create a FileSpecification for the attachment using the (filePath, description) ctor
            var fileSpec = new FileSpecification(attachmentFile, "Sample attachment");
            // Populate the Contents stream from the source file
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFile));
            // Add to the EmbeddedFiles collection (the correct API for attachments)
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF that now contains the attachment
            doc.Save(outputPdf);
        }

        swAdd.Stop();
        Console.WriteLine($"Add attachment time: {swAdd.ElapsedMilliseconds} ms");

        // -----------------------------------------------------------------
        // 2. Extract attachments and measure time
        // -----------------------------------------------------------------
        var swExtract = Stopwatch.StartNew();

        using (Document doc = new Document(outputPdf))
        {
            // Iterate over all embedded files using foreach (the collection is keyed by name, not by int index)
            foreach (FileSpecification spec in doc.EmbeddedFiles)
            {
                string outPath = Path.Combine(extractDir, spec.Name); // Name holds the original file name

                // Ensure the stream is positioned at the beginning
                if (spec.Contents.CanSeek)
                    spec.Contents.Position = 0;

                // Write the attached file to disk
                using (FileStream outStream = File.Create(outPath))
                {
                    spec.Contents.CopyTo(outStream);
                }
            }
        }

        swExtract.Stop();
        Console.WriteLine($"Extract attachments time: {swExtract.ElapsedMilliseconds} ms");

        // -----------------------------------------------------------------
        // 3. Remove attachments and measure time
        // -----------------------------------------------------------------
        var swRemove = Stopwatch.StartNew();

        using (Document doc = new Document(outputPdf))
        {
            // The EmbeddedFiles collection expects a string (the attachment name) for deletion.
            // Collect the names first to avoid modifying the collection while iterating.
            var names = new System.Collections.Generic.List<string>();
            foreach (FileSpecification spec in doc.EmbeddedFiles)
                names.Add(spec.Name);

            foreach (string name in names)
                doc.EmbeddedFiles.Delete(name);

            // Save the PDF without attachments
            doc.Save("output_without_attachments.pdf");
        }

        swRemove.Stop();
        Console.WriteLine($"Remove attachments time: {swRemove.ElapsedMilliseconds} ms");
    }
}
