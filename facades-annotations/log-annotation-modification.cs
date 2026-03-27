using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages and their annotations
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    DateTime now = DateTime.Now;
                    annotation.Modified = now;
                    Console.WriteLine($"Page {page.Number}, Annotation Type {annotation.AnnotationType}, Modified set to {now}");
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}