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

        // Create a new PDF document (empty)
        using (Document pdf = new Document())
        {
            // Optional: add a blank page so the PDF has visible content
            pdf.Pages.Add();

            // Embed the Word document into the PDF portfolio
            // Create a FileSpecification and assign its Contents via a stream
            var fileSpec = new FileSpecification(wordPath, "Word document");
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(wordPath));
            pdf.EmbeddedFiles.Add(fileSpec);

            // Save the PDF; the embedded file makes it a portfolio
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created at '{outputPdf}'.");
    }
}
