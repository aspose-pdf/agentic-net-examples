using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDoc = "output.doc";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Convert to DOC format.
            DocSaveOptions docOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.Doc,               // .doc output
                Mode = DocSaveOptions.RecognitionMode.Flow,         // full flow recognition
                RecognizeBullets = true,                            // enable bullet detection
                RelativeHorizontalProximity = 2.5f                  // fine‑tune word spacing
            };
            pdfDoc.Save(outputDoc, docOptions);

            // Convert to DOCX format.
            DocSaveOptions docxOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX,              // .docx output
                Mode = DocSaveOptions.RecognitionMode.Flow,
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f
            };
            pdfDoc.Save(outputDocx, docxOptions);
        }

        Console.WriteLine($"Conversion completed: {outputDoc}, {outputDocx}");
    }
}