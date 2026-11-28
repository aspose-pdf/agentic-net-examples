using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "TextOnly.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPdf);

        var textAnnotations = new List<string>();

        // Iterate through all pages (Aspose.Pdf pages are 1‑based).
        for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
        {
            Page page = doc.Pages[pageNumber];
            foreach (Annotation annot in page.Annotations)
            {
                // Keep only Text (sticky‑note) annotations that have contents.
                if (annot.AnnotationType == AnnotationType.Text && !string.IsNullOrEmpty(annot.Contents))
                {
                    textAnnotations.Add(annot.Contents);
                }
            }
        }

        // Write the collected annotation texts to a plain‑text file.
        File.WriteAllLines(outputTxt, textAnnotations);
        Console.WriteLine($"Extracted {textAnnotations.Count} text annotation(s) to '{outputTxt}'.");
    }
}
