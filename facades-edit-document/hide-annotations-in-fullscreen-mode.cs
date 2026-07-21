using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (Facades) to read the viewer preference flags
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        int viewerPref = editor.GetViewerPreference();

        // Check if FullScreen mode is enabled
        bool isFullScreen = (viewerPref & ViewerPreference.PageModeFullScreen) != 0;

        // Load the document for annotation manipulation
        using (Document doc = new Document(inputPath))
        {
            if (isFullScreen)
            {
                // Iterate all pages and annotations, setting the Hidden flag
                foreach (Page page in doc.Pages)
                {
                    // Annotation collections are 1‑based
                    for (int i = 1; i <= page.Annotations.Count; i++)
                    {
                        Annotation ann = page.Annotations[i];
                        ann.Flags = ann.Flags | AnnotationFlags.Hidden;
                    }
                }
            }

            // Save the (possibly modified) PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}