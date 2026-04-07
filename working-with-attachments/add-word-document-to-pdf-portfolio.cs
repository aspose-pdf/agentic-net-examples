using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the output PDF portfolio and the Word document to embed
        const string outputPdfPath = "portfolio.pdf";
        const string wordDocPath   = "document.docx";

        // Verify the Word document exists before proceeding
        if (!File.Exists(wordDocPath))
        {
            Console.Error.WriteLine($"Word document not found: {wordDocPath}");
            return;
        }

        // Create a new PDF document (empty) and ensure it has at least one page
        using (Document pdfDoc = new Document())
        {
            // A PDF portfolio requires a page; add a blank one if none exist
            pdfDoc.Pages.Add();

            // Create a file specification for the Word document
            FileSpecification wordFileSpec = new FileSpecification(wordDocPath);

            // Add the Word document to the PDF portfolio (collection)
            pdfDoc.Collection.Add(wordFileSpec);

            // Save the PDF portfolio to disk
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF portfolio created at '{outputPdfPath}' with embedded Word document.");
    }
}