using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Get total number of pages from the bound document
        Document boundDoc = editor.Document;
        int pageCount = boundDoc.Pages.Count;

        // Extract all annotations (passing null for types returns all)
        IList<Annotation> annotationList = editor.ExtractAnnotations(1, pageCount, (string[])null);

        Console.WriteLine($"Total annotations found: {annotationList.Count}");
        foreach (Annotation annotation in annotationList)
        {
            // Annotation.Name holds the unique name identifier
            Console.WriteLine($"Annotation Name: {annotation.Name}");
        }

        // Clean up the facade
        editor.Close();
    }
}