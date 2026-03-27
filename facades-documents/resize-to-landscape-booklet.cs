using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string rotatedPath = "rotated.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Rotate all pages to landscape orientation
        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Rotate 90 degrees clockwise using the Rotation enum
                page.Rotate = Rotation.on90;
            }

            doc.Save(rotatedPath);
        }

        // Generate booklet from the rotated PDF
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(rotatedPath, outputPath);
        if (success)
        {
            Console.WriteLine($"Booklet created: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}
