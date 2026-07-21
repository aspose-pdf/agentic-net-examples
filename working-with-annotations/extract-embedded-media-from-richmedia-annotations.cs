using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputRoot = "ExtractedMedia";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root output folder exists.
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based.
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Annotations collection is also 1‑based.
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Process only RichMediaAnnotation instances.
                    if (ann is RichMediaAnnotation richAnn)
                    {
                        // Create a folder hierarchy: /ExtractedMedia/Page_{n}/Annotation_{i}
                        string annFolder = Path.Combine(outputRoot, $"Page_{pageNum}", $"Annotation_{annIdx}");
                        Directory.CreateDirectory(annFolder);

                        // Determine a file name. Use the annotation's Name if set; otherwise generate a GUID.
                        string fileName = richAnn.Name;
                        if (string.IsNullOrWhiteSpace(fileName))
                        {
                            fileName = Guid.NewGuid().ToString() + ".bin";
                        }

                        string outPath = Path.Combine(annFolder, fileName);

                        // The Content property provides a Stream with the embedded media.
                        // Guard against null streams.
                        using (Stream contentStream = richAnn.Content)
                        {
                            if (contentStream != null)
                            {
                                // Ensure the stream is positioned at the start.
                                if (contentStream.CanSeek)
                                    contentStream.Position = 0;

                                // Write the stream to a file.
                                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                                {
                                    contentStream.CopyTo(fs);
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Media extraction completed.");
    }
}