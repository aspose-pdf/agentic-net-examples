using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ----- Remove document‑level JavaScript actions -----
            if (doc.JavaScript != null)
            {
                // Keys is a snapshot; copy to a list before removal
                var keys = doc.JavaScript.Keys.ToList();
                foreach (var key in keys)
                {
                    doc.JavaScript.Remove(key);
                }
            }

            // ----- Remove page‑level JavaScript actions attached to annotations -----
            foreach (Page page in doc.Pages)
            {
                // Iterate backwards to be safe if the collection size changes
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];
                    // Only LinkAnnotation (and its derived types) expose an Action property
                    if (ann is LinkAnnotation link && link.Action is JavascriptAction)
                    {
                        link.Action = null;
                    }
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All JavaScript actions removed. Saved to '{outputPath}'.");
    }
}
