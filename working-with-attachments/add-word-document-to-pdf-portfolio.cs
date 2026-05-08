using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string wordPath = "sample.docx";   // input Word document
        const string outputPdf = "portfolio.pdf"; // output PDF Portfolio

        if (!File.Exists(wordPath))
        {
            Console.Error.WriteLine($"Word file not found: {wordPath}");
            return;
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Ensure the document has a collection (portfolio) initialized
            if (pdfDoc.Collection == null)
                pdfDoc.Collection = new Collection();

            // Create a FileSpecification for the Word document
            var fileSpec = new FileSpecification(Path.GetFileName(wordPath), "Embedded Word document")
            {
                // Load the file bytes into the specification
                Contents = new MemoryStream(File.ReadAllBytes(wordPath))
            };

            // Add the file specification to the PDF portfolio collection
            pdfDoc.Collection.Add(fileSpec);

            // Save the PDF Portfolio
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF Portfolio created: {outputPdf}");
    }
}
