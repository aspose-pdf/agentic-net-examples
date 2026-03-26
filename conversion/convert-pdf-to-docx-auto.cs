using System;
using System.IO;
using Aspose.Pdf; // Save option classes are now in the root Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDoc = new Document(inputPath))
        {
            // Automatic content detection is the default behaviour.
            var saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX // ensure DOCX output
            };

            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to DOCX: {outputPath}");
    }
}