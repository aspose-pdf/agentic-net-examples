using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDoc = "output.doc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document pdfDoc = new Document(inputPdf))
        {
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.Doc,
                // Use the Textbox recognition mode to extract plain text layout
                Mode = DocSaveOptions.RecognitionMode.Textbox
            };

            pdfDoc.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF converted to DOC: {outputDoc}");
    }
}