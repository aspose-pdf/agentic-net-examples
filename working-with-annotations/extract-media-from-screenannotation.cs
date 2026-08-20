using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedMedia";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            int mediaCount = 0;

            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Process only ScreenAnnotation instances
                    if (ann is ScreenAnnotation screenAnn)
                    {
                        // The Action of a ScreenAnnotation that references an external file is a GoToRemoteAction
                        if (screenAnn.Action is GoToRemoteAction goToRemote && goToRemote.File != null)
                        {
                            // Retrieve the embedded file specification
                            FileSpecification fileSpec = goToRemote.File;

                            // The actual media data is stored in the Contents stream of the FileSpecification
                            using (Stream mediaStream = fileSpec.Contents)
                            {
                                // Determine a file extension – fall back to .bin if the name is unavailable
                                string extension = Path.GetExtension(fileSpec.Name ?? string.Empty);
                                if (string.IsNullOrEmpty(extension))
                                    extension = ".bin";

                                // Build a unique file name for the extracted media
                                string mediaFileName = $"media_{++mediaCount}{extension}";
                                string outputPath = Path.Combine(outputDir, mediaFileName);

                                // Write the media stream to disk
                                using (FileStream outFs = File.Create(outputPath))
                                {
                                    mediaStream.CopyTo(outFs);
                                }

                                Console.WriteLine($"Extracted media saved to: {outputPath}");
                            }
                        }
                    }
                }
            }

            if (mediaCount == 0)
            {
                Console.WriteLine("No ScreenAnnotation media found in the document.");
            }
        }
    }
}
