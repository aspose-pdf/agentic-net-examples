using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "AllAnnotations.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPdf);

        var sb = new StringBuilder();

        // Iterate through all pages and collect FreeText and Text annotation contents.
        foreach (Page page in pdfDocument.Pages)
        {
            foreach (Annotation annotation in page.Annotations)
            {
                switch (annotation)
                {
                    case TextAnnotation textAnno:
                        if (!string.IsNullOrEmpty(textAnno.Contents))
                            sb.AppendLine(textAnno.Contents);
                        break;
                    case FreeTextAnnotation freeTextAnno:
                        if (!string.IsNullOrEmpty(freeTextAnno.Contents))
                            sb.AppendLine(freeTextAnno.Contents);
                        break;
                }
            }
        }

        // Write the collected annotation texts to the output file.
        File.WriteAllText(outputTxt, sb.ToString());
        Console.WriteLine($"Annotations extracted to '{outputTxt}'.");
    }
}
