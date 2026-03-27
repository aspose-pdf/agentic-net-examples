using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Flatten all annotations in the document
            doc.Flatten();

            // Scan each page to verify that no annotations remain
            bool anyAnnotations = false;
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                if (page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    Console.WriteLine($"Page {i} still has {page.Annotations.Count} annotation(s).");
                }
            }

            if (!anyAnnotations)
            {
                Console.WriteLine("All annotations successfully flattened.");
            }

            // Save the flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}