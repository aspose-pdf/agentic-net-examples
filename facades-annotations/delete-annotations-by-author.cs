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
        const string outputPath = "output_filtered.pdf";
        const string authorToDelete = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect the names of annotations whose Title (author) matches the target
            List<string> namesToDelete = new List<string>();

            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    // Title exists only on markup annotations, so cast first
                    if (annot is MarkupAnnotation markup &&
                        !string.IsNullOrEmpty(markup.Title) &&
                        markup.Title.Equals(authorToDelete, StringComparison.OrdinalIgnoreCase))
                    {
                        // Name property uniquely identifies the annotation
                        if (!string.IsNullOrEmpty(annot.Name))
                            namesToDelete.Add(annot.Name);
                    }
                }
            }

            // Use PdfAnnotationEditor (Facades API) to delete the selected annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc); // bind the loaded document

                foreach (string annotName in namesToDelete)
                {
                    editor.DeleteAnnotation(annotName);
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
    }
}
