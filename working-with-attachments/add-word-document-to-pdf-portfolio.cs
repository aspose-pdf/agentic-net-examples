using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string wordPath = "sample.docx";
        const string outputPdf = "portfolio.pdf";

        if (!File.Exists(wordPath))
        {
            Console.Error.WriteLine($"Word file not found: {wordPath}");
            return;
        }

        // Create an empty PDF document (the portfolio container)
        using (Document pdfDoc = new Document())
        {
            // Create a file specification for the Word document to embed
            FileSpecification fileSpec = new FileSpecification(wordPath);

            // Add the Word document to the PDF portfolio (embedded files collection)
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the resulting PDF portfolio
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created at '{outputPdf}'.");
    }
}