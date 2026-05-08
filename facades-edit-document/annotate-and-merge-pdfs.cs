using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";   // PDF to annotate
        const string secondPdfPath = "second.pdf";  // PDF to merge after the first
        const string editedPdfPath = "first_annotated.pdf";
        const string outputPdfPath = "merged_output.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Add a text annotation to the first PDF
        // -------------------------------------------------
        PdfContentEditor annotationEditor = new PdfContentEditor();
        annotationEditor.BindPdf(firstPdfPath);

        // Create a text annotation on page 1
        // System.Drawing.Rectangle is required for the geometry
        annotationEditor.CreateText(
            new System.Drawing.Rectangle(100, 500, 300, 550), // left, top, width, height
            "Note",                                          // title
            "This PDF has been annotated.",                  // contents
            true,                                            // open by default
            "Note",                                          // icon name
            1);                                              // page number (1‑based)

        // Save the annotated PDF to a temporary file
        annotationEditor.Save(editedPdfPath);
        annotationEditor.Close();

        // -------------------------------------------------
        // Step 2: Merge the second PDF after the annotated PDF
        // -------------------------------------------------
        using (Document target = new Document(editedPdfPath))
        using (Document source = new Document(secondPdfPath))
        {
            // Append all pages from the source document
            target.Pages.Add(source.Pages);

            // Save the combined document
            target.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}