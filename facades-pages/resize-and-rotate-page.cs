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

        using (Document doc = new Document(inputPath))
        {
            PdfFileEditor fileEditor = new PdfFileEditor();

            PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
                PdfFileEditor.ContentsResizeValue.Percents(10), // left margin 10%
                null,                                         // auto width
                PdfFileEditor.ContentsResizeValue.Percents(10), // right margin 10%
                PdfFileEditor.ContentsResizeValue.Percents(10), // top margin 10%
                null,                                         // auto height
                PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin 10%
            );

            // Resize contents of the first page (page index starts at 1)
            fileEditor.ResizeContents(doc, new int[] { 1 }, parameters);

            // Rotate the first page by 90 degrees using the correct enum value
            doc.Pages[1].Rotate = Rotation.on90;

            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}