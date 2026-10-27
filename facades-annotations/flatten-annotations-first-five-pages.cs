using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_first5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        Document srcDoc = new Document(inputPath);

        // ---------------------------------------------------------------------
        // 1. Extract the first five pages (or fewer if the document is shorter)
        // ---------------------------------------------------------------------
        Document firstPart = new Document();
        int maxPage = Math.Min(5, srcDoc.Pages.Count);
        for (int i = 1; i <= maxPage; i++)
        {
            firstPart.Pages.Add(srcDoc.Pages[i]);
        }

        // ---------------------------------------------------------------
        // 2. Flatten annotations on the extracted pages using PdfAnnotationEditor
        // ---------------------------------------------------------------
        using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
        {
            annotEditor.BindPdf(firstPart);          // bind the Document instance
            annotEditor.FlatteningAnnotations();     // correct method name
        }

        // ---------------------------------------------------------------
        // 3. Build the final document: flattened first part + remaining pages unchanged
        // ---------------------------------------------------------------
        Document result = new Document();
        result.Pages.Add(firstPart.Pages); // add the flattened first‑five pages

        // Append the rest of the original document (if any)
        for (int i = maxPage + 1; i <= srcDoc.Pages.Count; i++)
        {
            result.Pages.Add(srcDoc.Pages[i]);
        }

        // Save the combined PDF
        result.Save(outputPath);
        Console.WriteLine($"Annotations on pages 1‑{maxPage} have been flattened. Output saved to '{outputPath}'.");
    }
}
