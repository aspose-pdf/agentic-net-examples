using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetAuthor = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Collect the names of annotations whose Title (author) matches the target
        List<string> namesToDelete = new List<string>();

        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    // Title is defined only on markup annotations (e.g., TextAnnotation, FreeTextAnnotation, etc.)
                    if (annot is MarkupAnnotation markup && markup.Title == targetAuthor && !string.IsNullOrEmpty(annot.Name))
                    {
                        namesToDelete.Add(annot.Name);
                    }
                }
            }
        }

        // Delete the collected annotations using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            foreach (string name in namesToDelete)
            {
                editor.DeleteAnnotation(name);
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations authored by \"{targetAuthor}\" have been removed. Output saved to '{outputPath}'.");
    }
}
