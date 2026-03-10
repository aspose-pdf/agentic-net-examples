using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string modifiedPdf = "modified.pdf";
        const string outputDoc = "output.doc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Manipulate pages using the PdfPageEditor facade
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(pdfDoc);

            // Example manipulation: rotate the first page 90 degrees
            pageEditor.Rotation = 90;
            pageEditor.ProcessPages = new int[] { 1 }; // apply only to page 1
            pageEditor.ApplyChanges();

            // Save the modified PDF (preserves layout)
            pdfDoc.Save(modifiedPdf);

            // Convert the modified PDF to DOC format while preserving layout
            DocSaveOptions docOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.Doc,
                Mode = DocSaveOptions.RecognitionMode.Flow,
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };
            pdfDoc.Save(outputDoc, docOptions);
        }

        Console.WriteLine("Page manipulation and DOC conversion completed successfully.");
    }
}