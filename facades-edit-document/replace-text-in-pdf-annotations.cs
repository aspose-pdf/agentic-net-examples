using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the text to replace inside annotations
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcText    = "OldText";
        const string destText   = "NewText";

        // Pages on which the replacement should be performed (1‑based indexing)
        int[] targetPages = { 1, 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor is a Facade class – wrap it in a using block for deterministic disposal
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the existing PDF file to the editor
                editor.BindPdf(inputPath);

                // Iterate over the requested pages
                foreach (int pageNumber in targetPages)
                {
                    // Guard against invalid page numbers
                    if (pageNumber < 1 || pageNumber > editor.Document.Pages.Count)
                        continue;

                    Page page = editor.Document.Pages[pageNumber];

                    // Annotations collection uses 1‑based indexing as well
                    for (int idx = 1; idx <= page.Annotations.Count; idx++)
                    {
                        Annotation annotation = page.Annotations[idx];

                        // Only modify the Contents property of annotations that contain the source text
                        if (!string.IsNullOrEmpty(annotation.Contents) && annotation.Contents.Contains(srcText))
                        {
                            annotation.Contents = annotation.Contents.Replace(srcText, destText);
                        }
                    }
                }

                // Save the modified PDF – this writes only the changed annotation data,
                // the main page content remains untouched.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}