using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDoc = "output.doc";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: create/load)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // Manipulate pages using the PdfPageEditor facade
            // -----------------------------------------------------------------
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded PDF document to the facade
                editor.BindPdf(pdfDoc);

                // Example manipulation: rotate the first page 90 degrees
                editor.ProcessPages = new int[] { 1 }; // target page numbers (1‑based)
                editor.Rotation = 90;                  // allowed values: 0,90,180,270

                // Example manipulation: increase zoom to 120%
                editor.Zoom = 1.2f; // 1.0 = 100%

                // Apply the changes to the bound document
                editor.ApplyChanges();
                // No explicit save needed; changes are reflected in pdfDoc
            }

            // -----------------------------------------------------------------
            // Convert the (now manipulated) PDF to DOC format preserving layout
            // -----------------------------------------------------------------
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Output as classic .doc (could also use DocFormat.Docx)
                Format = DocSaveOptions.DocFormat.Doc,

                // Use Flow recognition mode to retain logical flow while keeping layout
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Save the document using SaveOptions (lifecycle: save)
            pdfDoc.Save(outputDoc, docOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOC: {outputDoc}");
    }
}