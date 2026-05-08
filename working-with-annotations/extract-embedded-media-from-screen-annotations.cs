using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF containing ScreenAnnotation(s)
        const string outputDir = "ExtractedMedia";     // folder to store extracted files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF (lifecycle rule: use using block)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all annotations on the page
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Process only ScreenAnnotation instances
                    if (ann is ScreenAnnotation screenAnn)
                    {
                        // The Action property holds the media reference.
                        // For a screen annotation it is typically a GoToRemoteAction.
                        if (screenAnn.Action is GoToRemoteAction goToRemote)
                        {
                            // FileSpecification contains the embedded media.
                            FileSpecification fileSpec = goToRemote.File;

                            // Use the original file name if available; otherwise generate one.
                            string mediaFileName = string.IsNullOrEmpty(fileSpec.Name)
                                ? $"media_page{pageNum}_ann{annIdx}"
                                : Path.GetFileName(fileSpec.Name);

                            string outputPath = Path.Combine(outputDir, mediaFileName);

                            // Extract the embedded stream and write it to disk.
                            using (Stream mediaStream = fileSpec.Contents)
                            using (FileStream outFs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                mediaStream.CopyTo(outFs);
                            }

                            Console.WriteLine($"Extracted media from page {pageNum}, annotation {annIdx} to '{outputPath}'.");
                        }
                    }
                }
            }

            // No modifications made; just demonstrate proper disposal.
            // (Saving not required for extraction, but lifecycle rule mandates using block.)
        }
    }
}
