using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf.Document (PdfAnnotationEditor does not expose a GetAnnotations method)
        Document doc = new Document(inputPath);

        int totalAnnotations = 0;
        foreach (Page page in doc.Pages)
        {
            foreach (Annotation annotation in page.Annotations)
            {
                totalAnnotations++;
                // Annotation.Name contains the annotation's name (or an empty string if not set)
                Console.WriteLine($"- Name: {annotation.Name}");
            }
        }

        Console.WriteLine($"Found {totalAnnotations} annotation(s) in the document.");
    }
}
