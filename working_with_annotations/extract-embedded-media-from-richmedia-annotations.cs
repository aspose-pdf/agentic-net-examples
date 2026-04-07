using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputRoot   = "ExtractedMedia";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRoot);

        // Open the PDF document (deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Iterate through all annotations on the page
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Process only RichMediaAnnotation instances
                    if (ann is RichMediaAnnotation richMedia)
                    {
                        // Use the annotation Name if available; otherwise generate a unique name
                        string annName = !string.IsNullOrEmpty(richMedia.Name)
                                         ? richMedia.Name
                                         : $"RichMedia_{annIdx}";

                        // Create a subfolder for this page (e.g., Page_1) and annotation
                        string pageFolder = Path.Combine(outputRoot, $"Page_{pageIdx}");
                        string annFolder  = Path.Combine(pageFolder, annName);
                        Directory.CreateDirectory(annFolder);

                        // The Content property provides the embedded media stream
                        // Copy it to a file preserving the original file name if possible
                        // If the original file name is unknown, use a generic name with appropriate extension
                        string fileName = $"{annName}.bin"; // fallback name
                        // Attempt to infer extension from the ContentType enum
                        switch (richMedia.Type)
                        {
                            case RichMediaAnnotation.ContentType.Audio:
                                fileName = $"{annName}.mp3";
                                break;
                            case RichMediaAnnotation.ContentType.Video:
                                fileName = $"{annName}.mp4";
                                break;
                        }

                        string outputPath = Path.Combine(annFolder, fileName);

                        // Write the stream to disk
                        using (Stream contentStream = richMedia.Content)
                        using (FileStream fileStream = File.Create(outputPath))
                        {
                            contentStream.CopyTo(fileStream);
                        }

                        Console.WriteLine($"Extracted media to: {outputPath}");
                    }
                }
            }
        }
    }
}