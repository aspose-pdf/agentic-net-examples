using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF containing RichMediaAnnotations
        const string outputRoot = "ExtractedMedia";        // base folder for extracted files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Iterate over all annotations on the current page
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Process only RichMediaAnnotation instances
                    if (ann is RichMediaAnnotation richMedia)
                    {
                        // Build a folder hierarchy: OutputRoot/Page_{pageIdx}/Annotation_{annIdx}
                        string pageFolder = Path.Combine(outputRoot, $"Page_{pageIdx}");
                        string annotationFolder = Path.Combine(pageFolder, $"Annotation_{annIdx}");
                        Directory.CreateDirectory(annotationFolder);

                        // Determine a file name based on the annotation's type
                        string extension = richMedia.Type switch
                        {
                            RichMediaAnnotation.ContentType.Audio => ".mp3",
                            RichMediaAnnotation.ContentType.Video => ".mp4",
                            _ => ".bin"
                        };

                        // Use the annotation's Name if available; otherwise a default name
                        string baseName = !string.IsNullOrEmpty(richMedia.Name) ? richMedia.Name : "media";
                        string outputFile = Path.Combine(annotationFolder, baseName + extension);

                        // The Content property returns a stream with the embedded media data
                        // Copy the stream to the output file
                        using (Stream contentStream = richMedia.Content)
                        using (FileStream fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                        {
                            contentStream.CopyTo(fileStream);
                        }

                        Console.WriteLine($"Extracted: {outputFile}");
                    }
                }
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}