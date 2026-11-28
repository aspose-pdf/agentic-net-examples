using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "AnnotationCount.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPdf);

            // Count annotations across all pages
            int totalAnnotations = 0;
            foreach (Page page in doc.Pages)
            {
                totalAnnotations += page.Annotations?.Count ?? 0;
            }

            // Write the count to a text file
            File.WriteAllText(outputTxt, totalAnnotations.ToString());

            Console.WriteLine($"Total annotations extracted: {totalAnnotations}");
            Console.WriteLine($"Count written to {outputTxt}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
