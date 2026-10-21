using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_custom_flags.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Iterate through all annotations on the page and set custom flags
            foreach (Annotation ann in page.Annotations)
            {
                // Extend the annotation flags – example adds the NoView flag
                // (combine with existing flags using bitwise OR)
                ann.Flags = ann.Flags | AnnotationFlags.NoView;
            }

            // Save the document – the modified flags are persisted automatically.
            doc.Save(outputPath);

            Console.WriteLine($"Annotations updated with custom flags and saved to '{outputPath}'.");
        }
    }
}
