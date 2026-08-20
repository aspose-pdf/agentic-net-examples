using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "renamed_annotations.pdf";
        const string prefix    = "StdPrefix_";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the annotation editor and bind the source PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);

            // Access the underlying Document to iterate pages and annotations
            Document doc = editor.Document;

            // Rename each annotation by adding the standardized prefix
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    // Only rename if the annotation already has a name
                    if (!string.IsNullOrEmpty(annot.Name))
                    {
                        annot.Name = prefix + annot.Name;
                    }
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations renamed and saved to '{outputPdf}'.");
    }
}