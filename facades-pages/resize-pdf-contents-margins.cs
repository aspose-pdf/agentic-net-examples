using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            PdfFileEditor fileEditor = new PdfFileEditor();

            PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
                PdfFileEditor.ContentsResizeValue.Percents(10),
                null,
                PdfFileEditor.ContentsResizeValue.Percents(10),
                PdfFileEditor.ContentsResizeValue.Percents(10),
                null,
                PdfFileEditor.ContentsResizeValue.Percents(10)
            );

            fileEditor.ResizeContents(document, parameters);
            document.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}