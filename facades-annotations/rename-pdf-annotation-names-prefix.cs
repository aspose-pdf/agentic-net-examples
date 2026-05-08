using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_renamed.pdf";
        const string prefix     = "Std_";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        // Create a PdfAnnotationEditor facade and bind it to the loaded document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(doc);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the current page
                for (int annotIndex = 1; annotIndex <= page.Annotations.Count; annotIndex++)
                {
                    Annotation annot = page.Annotations[annotIndex];

                    // Preserve the original name if it exists; otherwise generate one
                    string originalName = !string.IsNullOrEmpty(annot.Name)
                                          ? annot.Name
                                          : $"Annot_{pageIndex}_{annotIndex}";

                    // Apply the standardized prefix
                    annot.Name = prefix + originalName;
                }
            }

            // Save the modified PDF using the facade's Save method
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations renamed and saved to '{outputPath}'.");
    }
}