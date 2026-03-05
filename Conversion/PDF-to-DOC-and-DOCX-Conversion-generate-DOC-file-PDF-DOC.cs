using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF and target DOC/DOCX files
        const string pdfPath   = "input.pdf";
        const string docPath   = "output.doc";
        const string docxPath  = "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: create/load)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // ---------- Convert to DOC ----------
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Choose DOC output format
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Flow recognition mode for editable output
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition (optional but common)
                RecognizeBullets = true,
                // Adjust horizontal proximity for text grouping
                RelativeHorizontalProximity = 2.5f
            };

            // Save as DOC (lifecycle: save)
            pdfDocument.Save(docPath, docOptions);

            // ---------- Convert to DOCX ----------
            DocSaveOptions docxOptions = new DocSaveOptions
            {
                // Choose DOCX output format
                Format = DocSaveOptions.DocFormat.DocX,
                Mode = DocSaveOptions.RecognitionMode.Flow,
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };

            // Save as DOCX
            pdfDocument.Save(docxPath, docxOptions);
        }

        Console.WriteLine("PDF successfully converted to DOC and DOCX.");
    }
}