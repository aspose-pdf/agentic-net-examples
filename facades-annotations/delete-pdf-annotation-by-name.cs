using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "Comment1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the Document class (PdfFileEditor does not provide DeleteAnnotation).
        Document pdfDoc = new Document(inputPath);
        bool annotationRemoved = false;

        // Search each page for an annotation whose Name matches the target name and delete it.
        foreach (Page page in pdfDoc.Pages)
        {
            var annotation = page.Annotations.FirstOrDefault(a => a.Name == annotationName);
            if (annotation != null)
            {
                page.Annotations.Delete(annotation);
                annotationRemoved = true;
                break; // Assuming the name is unique, we can stop after the first match.
            }
        }

        if (!annotationRemoved)
        {
            Console.WriteLine($"Annotation '{annotationName}' not found.");
        }
        else
        {
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Annotation '{annotationName}' deleted. Output saved to '{outputPath}'.");
        }
    }
}
