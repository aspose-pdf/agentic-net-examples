using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "AnnotationsList.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Open the output text file
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                bool anyAnnotation = false;

                // Iterate through all pages
                foreach (Page page in pdfDocument.Pages)
                {
                    // Iterate through the annotations on the current page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        string content = annotation.Contents ?? string.Empty;
                        writer.WriteLine(content);
                        anyAnnotation = true;
                    }
                }

                // If no annotations were found, write a placeholder line (optional)
                if (!anyAnnotation)
                {
                    writer.WriteLine("(No annotations found)");
                }
            }

            Console.WriteLine($"Annotations saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
