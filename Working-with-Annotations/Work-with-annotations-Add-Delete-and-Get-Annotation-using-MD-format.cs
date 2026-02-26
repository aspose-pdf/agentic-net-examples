using System;
using System.Collections;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string tempPdf   = "temp_added.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -------------------------------------------------
        // 1. Add a text annotation (using PdfContentEditor)
        // -------------------------------------------------
        PdfContentEditor addEditor = new PdfContentEditor();
        addEditor.BindPdf(inputPdf);

        // Define the annotation rectangle (coordinates are in points)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a text annotation on page 1
        // title = author, contents = visible text, open = true, icon = "Note"
        addEditor.CreateText(rect, "John Doe", "This is a sample annotation added via MD format.", true, "Note", 1);

        // Save the document with the new annotation to a temporary file
        addEditor.Save(tempPdf);
        addEditor.Close();

        // -------------------------------------------------
        // 2. Delete all text annotations (using PdfAnnotationEditor)
        // -------------------------------------------------
        PdfAnnotationEditor delEditor = new PdfAnnotationEditor();
        delEditor.BindPdf(tempPdf);

        // Delete all annotations of type "Text"
        delEditor.DeleteAnnotations("Text");

        // -------------------------------------------------
        // 3. Extract remaining annotations and display their details
        // -------------------------------------------------
        // Extract annotations from pages 1 to the last page for a set of common types
        string[] typesToExtract = new string[] { "Text", "FreeText", "Ink", "Stamp", "Highlight", "Underline", "StrikeOut", "Squiggly" };
        ArrayList extracted = delEditor.ExtractAnnotations(1, delEditor.Document.Pages.Count, typesToExtract);

        Console.WriteLine("Extracted Annotations:");
        foreach (object obj in extracted)
        {
            // Cast each item to the base Annotation type
            Aspose.Pdf.Annotations.Annotation annot = obj as Aspose.Pdf.Annotations.Annotation;
            if (annot != null)
            {
                Console.WriteLine($"- Type   : {annot.AnnotationType}");
                Console.WriteLine($"  Title  : {annot.Title}");
                Console.WriteLine($"  Subject: {annot.Subject}");
                Console.WriteLine($"  Contents: {annot.Contents}");
                Console.WriteLine();
            }
        }

        // Save the final document (annotations deleted) to the output file
        delEditor.Save(outputPdf);
        delEditor.Close();

        Console.WriteLine($"Processing complete. Output saved to '{outputPdf}'.");
    }
}