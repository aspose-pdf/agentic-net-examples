using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExtractScreenMedia
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF containing ScreenAnnotation
        const string outputFolder = "ExtractedMedia";      // folder to save extracted media files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdf))
        {
            int mediaIndex = 0;

            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page (1‑based indexing)
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Check if the annotation is a ScreenAnnotation
                    if (ann is ScreenAnnotation screenAnn)
                    {
                        // The Action of a ScreenAnnotation is typically a GoToRemoteAction
                        if (screenAnn.Action is GoToRemoteAction goToRemote && goToRemote.File != null)
                        {
                            FileSpecification fileSpec = goToRemote.File;

                            // Retrieve the embedded media stream
                            using (Stream mediaStream = fileSpec.Contents)
                            {
                                // Determine a file extension (fallback to .bin if unknown)
                                string ext = Path.GetExtension(fileSpec.Name);
                                if (string.IsNullOrEmpty(ext))
                                    ext = ".bin";

                                // Build output file path
                                string outPath = Path.Combine(outputFolder,
                                    $"media_{++mediaIndex}{ext}");

                                // Write the media stream to disk
                                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                                {
                                    mediaStream.CopyTo(fs);
                                }

                                Console.WriteLine($"Extracted media saved to: {outPath}");
                            }
                        }
                    }
                }
            }

            if (mediaIndex == 0)
                Console.WriteLine("No ScreenAnnotation with embedded media found.");
        }
    }
}
