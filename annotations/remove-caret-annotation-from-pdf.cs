using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[2];

            // Iterate through annotations to find the CaretAnnotation titled "Obsolete"
            for (int i = 0; i < page.Annotations.Count; i++)
            {
                Annotation annot = page.Annotations[i];
                if (annot is CaretAnnotation caret && caret.Title == "Obsolete")
                {
                    // Remove the annotation from the collection
                    page.Annotations.Delete(i);
                    break; // Assuming only one such annotation exists
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Caret annotation removed and saved to '{outputPath}'.");
    }
}