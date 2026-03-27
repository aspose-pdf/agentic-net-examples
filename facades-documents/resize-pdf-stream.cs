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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Resize using the stream overload of PdfFileEditor
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream destinationStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor fileEditor = new PdfFileEditor();
            // Resize all pages (null pages array) to 1024 x 768 units
            bool resizeResult = fileEditor.ResizeContents(sourceStream, destinationStream, null, 1024.0, 768.0);
            Console.WriteLine($"Resize operation successful: {resizeResult}");
        }

        // Verify the new page dimensions
        using (Document resultDoc = new Document(outputPath))
        {
            if (resultDoc.Pages.Count > 0)
            {
                double pageWidth = resultDoc.Pages[1].PageInfo.Width;
                double pageHeight = resultDoc.Pages[1].PageInfo.Height;
                Console.WriteLine($"Result page size: {pageWidth} x {pageHeight}");
                const double expectedWidth = 1024.0;
                const double expectedHeight = 768.0;
                bool sizeMatches = Math.Abs(pageWidth - expectedWidth) < 0.01 && Math.Abs(pageHeight - expectedHeight) < 0.01;
                Console.WriteLine($"Size matches expected: {sizeMatches}");
            }
            else
            {
                Console.Error.WriteLine("No pages found in the resized document.");
            }
        }
    }
}
