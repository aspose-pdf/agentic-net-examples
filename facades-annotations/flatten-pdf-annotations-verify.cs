using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Flatten all annotations using the Facades API
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);                 // load PDF
        editor.FlatteningAnnotations();            // flatten annotations
        editor.Save(outputPath);                    // save flattened PDF
        editor.Close();                            // release resources

        // Verify that no annotation objects remain
        using (Document doc = new Document(outputPath)) // load the saved PDF
        {
            bool hasAnnotations = false;

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (doc.Pages[i].Annotations.Count > 0)
                {
                    hasAnnotations = true;
                    Console.WriteLine($"Page {i} still contains {doc.Pages[i].Annotations.Count} annotation(s).");
                }
            }

            Console.WriteLine(hasAnnotations
                ? "Annotations were not fully removed after flattening."
                : "All annotations successfully removed after flattening.");
        }
    }
}