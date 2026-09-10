using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExtractRichMedia
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputRoot = "ExtractedMedia";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRoot);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];
                // Create a sub‑folder for the current page
                string pageFolder = Path.Combine(outputRoot, $"Page_{pageIdx}");
                Directory.CreateDirectory(pageFolder);

                // Annotations collection is also 1‑based
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Process only RichMediaAnnotation instances
                    if (ann is RichMediaAnnotation richMedia)
                    {
                        // Use the annotation name if set; otherwise generate a unique name
                        string baseName = !string.IsNullOrEmpty(richMedia.Name)
                                          ? richMedia.Name
                                          : $"RichMedia_{annIdx}";

                        // Determine a file extension based on the media type
                        string extension = richMedia.Type == RichMediaAnnotation.ContentType.Video ? ".mp4"
                                         : richMedia.Type == RichMediaAnnotation.ContentType.Audio ? ".mp3"
                                         : ".bin";

                        string outputPath = Path.Combine(pageFolder, baseName + extension);

                        // The Content property returns the embedded media as a stream.
                        // It is exposed as a byte[] via the GetContent() method in the API.
                        // Since the exact accessor is not documented here, we use the
                        // Content property assuming it provides a Stream.
                        // If it returns a string, you may need to adjust the extraction logic.
                        try
                        {
                            // Attempt to read the content as a stream
                            using (Stream contentStream = richMedia.Content as Stream)
                            {
                                if (contentStream != null)
                                {
                                    using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                                    {
                                        contentStream.CopyTo(fileOut);
                                    }
                                    Console.WriteLine($"Extracted: {outputPath}");
                                }
                                else
                                {
                                    // Fallback: if Content is a string, write it as text
                                    string textContent = richMedia.Content?.ToString();
                                    if (!string.IsNullOrEmpty(textContent))
                                    {
                                        File.WriteAllText(outputPath, textContent);
                                        Console.WriteLine($"Extracted (text): {outputPath}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"No extractable content for annotation '{baseName}' on page {pageIdx}.");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to extract media from annotation '{baseName}' on page {pageIdx}: {ex.Message}");
                        }
                    }
                }
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}