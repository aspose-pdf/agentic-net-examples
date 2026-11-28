using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "FreeText.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (the Facades PdfExtractor does not expose annotation APIs)
        Document pdfDocument = new Document(inputPath);

        using (StreamWriter writer = new StreamWriter(outputPath, false))
        {
            // Iterate through all pages and their annotations
            foreach (Page page in pdfDocument.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is FreeTextAnnotation freeText)
                    {
                        // The visible text of a FreeText annotation is stored in the Contents property
                        string text = freeText.Contents ?? string.Empty;
                        writer.WriteLine(text);
                    }
                }
            }
        }

        Console.WriteLine($"FreeText annotation contents saved to '{outputPath}'.");
    }
}
