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

        // Load the PDF document for annotation manipulation
        using (Document doc = new Document(inputPath))
        {
            // Bind the same file to PdfContentEditor to work with viewer preferences
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Retrieve current viewer preferences
            int viewerPref = editor.GetViewerPreference();

            // Check if FullScreen mode is enabled
            bool isFullScreen = (viewerPref & ViewerPreference.PageModeFullScreen) != 0;

            if (isFullScreen)
            {
                // Hide all annotations on every page
                foreach (Page page in doc.Pages)
                {
                    for (int i = 1; i <= page.Annotations.Count; i++) // 1‑based indexing
                    {
                        Annotation ann = page.Annotations[i];
                        ann.Flags |= AnnotationFlags.Hidden; // set Hidden flag
                    }
                }

                // Save the modified document via the editor (preserves viewer preferences)
                editor.Save(outputPath);
                Console.WriteLine($"Annotations hidden and saved to '{outputPath}'.");
            }
            else
            {
                // No FullScreen preference – just copy the original PDF
                doc.Save(outputPath);
                Console.WriteLine($"FullScreen not enabled; original PDF saved to '{outputPath}'.");
            }

            // Clean up the editor (it does not implement IDisposable)
            editor.Close();
        }
    }
}