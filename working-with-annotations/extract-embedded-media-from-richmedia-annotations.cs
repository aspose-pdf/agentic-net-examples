using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedMedia";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the base output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int annotationIndex = 0;

                // Iterate over annotations on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Identify RichMediaAnnotation instances
                    if (ann is RichMediaAnnotation richMedia)
                    {
                        annotationIndex++;

                        // Build a folder path: OutputDir/Page_{pageNum}/Annotation_{annotationIndex}
                        string pageFolder = Path.Combine(outputDir, $"Page_{pageNum}");
                        string annFolder  = Path.Combine(pageFolder, $"Annotation_{annotationIndex}");
                        Directory.CreateDirectory(annFolder);

                        // Determine a file name for the embedded media
                        // Prefer the annotation's Name; fall back to a generic name
                        string fileName = !string.IsNullOrEmpty(richMedia.Name)
                                          ? richMedia.Name
                                          : $"RichMedia_{annotationIndex}";

                        // Append appropriate extension if known (optional)
                        // Here we simply use .bin for unknown types
                        string filePath = Path.Combine(annFolder, $"{fileName}.bin");

                        // The Content property returns a Stream containing the media data
                        // Copy the stream to a file on disk
                        using (Stream contentStream = richMedia.Content)
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            contentStream.CopyTo(fileStream);
                        }

                        Console.WriteLine($"Extracted media to: {filePath}");
                    }
                }
            }
        }

        Console.WriteLine("Media extraction completed.");
    }
}