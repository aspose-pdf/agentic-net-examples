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
        const int targetPage = 1; // 1‑based page number

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Document implements IDisposable)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Validate page number
            if (targetPage < 1 || targetPage > pdfDocument.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Get the specified page
            Page page = pdfDocument.Pages[targetPage];

            // Find the first Line annotation on the page
            LineAnnotation lineAnno = null;
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is LineAnnotation la)
                {
                    lineAnno = la;
                    break;
                }
            }

            if (lineAnno == null)
            {
                Console.Error.WriteLine("Line annotation not found on the specified page.");
                return;
            }

            // Change the annotation color to red
            lineAnno.Color = Color.Red;

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Line annotation color changed to red and saved to '{outputPath}'.");
    }
}
