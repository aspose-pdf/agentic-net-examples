using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputRoot = "ExtractedMedia";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // Process only RichMediaAnnotation instances
                    if (annotation is RichMediaAnnotation richMedia)
                    {
                        // Build a folder hierarchy: /ExtractedMedia/Page_{n}/Annotation_{m}_{Name}
                        string annFolder = Path.Combine(
                            outputRoot,
                            $"Page_{pageIndex}",
                            $"Annotation_{annIndex}_{richMedia.Name ?? "RichMedia"}");

                        Directory.CreateDirectory(annFolder);

                        // The Content property holds the embedded media stream
                        // (it returns a Stream; if null, nothing to extract)
                        Stream contentStream = richMedia.Content as Stream;

                        if (contentStream != null)
                        {
                            // Determine a file name for the extracted media
                            string fileName = richMedia.Name;
                            if (string.IsNullOrWhiteSpace(fileName))
                            {
                                // Fallback to a generic name based on the media type
                                fileName = richMedia.Type == RichMediaAnnotation.ContentType.Video
                                    ? "video.bin"
                                    : "audio.bin";
                            }
                            else if (!Path.HasExtension(fileName))
                            {
                                // Ensure the file has an extension
                                fileName += ".bin";
                            }

                            string outputPath = Path.Combine(annFolder, fileName);

                            // Write the stream to disk
                            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                contentStream.CopyTo(fileStream);
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("All embedded media files have been extracted.");
    }
}