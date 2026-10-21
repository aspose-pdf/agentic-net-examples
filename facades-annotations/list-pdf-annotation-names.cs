using System;
using System.IO;
using Aspose.Pdf;                 // Document, Page
using Aspose.Pdf.Annotations;   // Annotation

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the PDF file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        int totalAnnotations = 0;
        // Iterate through each page and collect annotations
        foreach (Page page in pdfDocument.Pages)
        {
            foreach (Annotation annotation in page.Annotations)
            {
                totalAnnotations++;
                // The Name property holds the annotation's identifier (may be empty for some types)
                Console.WriteLine($"Annotation Name: {annotation.Name}");
            }
        }

        // Output the number of annotations found
        Console.WriteLine($"Annotations found: {totalAnnotations}");
    }
}
