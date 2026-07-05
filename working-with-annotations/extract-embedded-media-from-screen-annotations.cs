using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF containing ScreenAnnotation
        const string outputDir  = "ExtractedMedia";    // folder to save extracted media

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (page indexing is 1‑based)
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                Page page = doc.Pages[p];

                // Iterate through annotations on the page (annotation collection is also 1‑based)
                for (int a = 1; a <= page.Annotations.Count; a++)
                {
                    Annotation ann = page.Annotations[a];

                    // Identify ScreenAnnotation instances
                    if (ann is ScreenAnnotation screenAnn)
                    {
                        // The Action property holds the media launch action.
                        // For a media file, Aspose.Pdf uses GoToRemoteAction with an embedded FileSpecification.
                        if (screenAnn.Action is GoToRemoteAction goToRemote && goToRemote.File != null)
                        {
                            // FileSpecification provides the original file name via the Name property.
                            string originalFileName = goToRemote.File.Name; // e.g., "video.mp4"

                            // Build a unique output path
                            string outputPath = Path.Combine(outputDir, originalFileName);
                            int copyIndex = 1;
                            while (File.Exists(outputPath))
                            {
                                // Avoid overwriting if multiple annotations reference files with the same name
                                string nameOnly = Path.GetFileNameWithoutExtension(originalFileName);
                                string ext      = Path.GetExtension(originalFileName);
                                outputPath = Path.Combine(outputDir, $"{nameOnly}_{copyIndex}{ext}");
                                copyIndex++;
                            }

                            // Extract the embedded media stream.
                            // The embedded file is accessed via FileSpecification.Contents which returns a Stream.
                            using (Stream mediaStream = goToRemote.File.Contents)
                            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                mediaStream.CopyTo(outStream);
                            }

                            Console.WriteLine($"Extracted media from page {p}, annotation {a} to '{outputPath}'.");
                        }
                        else
                        {
                            Console.WriteLine($"ScreenAnnotation on page {p}, annotation {a} has no embedded media action.");
                        }
                    }
                }
            }
        }
    }
}
