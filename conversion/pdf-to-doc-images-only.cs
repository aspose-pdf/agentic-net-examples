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
            var saveOptions = new DocSaveOptions
            {
                // Explicitly set the output format (optional – DocSaveOptions defaults to DOC)
                Format = DocSaveOptions.DocFormat.Doc,

                // Set a supported recognition mode. In the current Aspose.PDF versions only
                // the "Flow" mode is available. If a future version introduces an
                // "ImagesOnly" mode, replace the value accordingly.
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            pdfDoc.Save(outputDoc, saveOptions);
        }

        Console.WriteLine($"PDF converted to DOC: {outputDoc}");
    }
}
