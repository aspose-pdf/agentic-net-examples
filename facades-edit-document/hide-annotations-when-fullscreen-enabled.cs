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

        // Use PdfContentEditor to read current viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            int viewerPref = editor.GetViewerPreference();

            // Check if FullScreen mode is enabled
            bool isFullScreen = (viewerPref & ViewerPreference.PageModeFullScreen) != 0;

            // Load the document to modify annotation flags
            using (Document doc = new Document(inputPath))
            {
                if (isFullScreen)
                {
                    // Hide all annotations by setting the Hidden flag
                    foreach (Page page in doc.Pages)
                    {
                        for (int i = 1; i <= page.Annotations.Count; i++)
                        {
                            Annotation ann = page.Annotations[i];
                            ann.Flags |= AnnotationFlags.Hidden;
                        }
                    }
                }

                // Save the modified PDF to a temporary file
                string tempPath = Path.Combine(Path.GetDirectoryName(outputPath) ?? ".", "temp_modified.pdf");
                doc.Save(tempPath);
                // Rebind the editor to the modified file
                editor.BindPdf(tempPath);

                // Ensure the FullScreen viewer preference remains set
                editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

                // Save the final result
                editor.Save(outputPath);

                // Clean up the temporary file
                File.Delete(tempPath);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}