using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string authorToDelete = "John Doe";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable via SaveableFacade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // First, collect the names of annotations that match the author
            var namesToDelete = new List<string>();

            for (int pageNum = 1; pageNum <= editor.Document.Pages.Count; pageNum++)
            {
                Page page = editor.Document.Pages[pageNum];
                foreach (Annotation annotation in page.Annotations)
                {
                    // Title is defined on MarkupAnnotation, not on the base Annotation class
                    if (annotation is MarkupAnnotation markup && markup.Title == authorToDelete)
                    {
                        namesToDelete.Add(annotation.Name);
                    }
                }
            }

            // Delete the collected annotations (avoid modifying the collection while iterating)
            foreach (string name in namesToDelete)
            {
                editor.DeleteAnnotation(name);
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations authored by \"{authorToDelete}\" have been removed.");
    }
}
