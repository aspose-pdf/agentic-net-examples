using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document pdfDocument = new Document(inputPdf))
        {
            using (FileStream xfdfStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                pdfDocument.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Annotations exported to '{outputXfdf}'.");
    }
}