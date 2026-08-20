using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with read/write access so that incremental updates can be written.
        using (FileStream pdfStream = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        {
            // Load the document from the writable stream.
            Document doc = new Document(pdfStream);

            // Example modification: add a text annotation to the first page.
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Incremental update example",
                Open     = true,
                Icon     = TextIcon.Note,
                Color    = Aspose.Pdf.Color.Yellow
            };
            page.Annotations.Add(annotation);

            // Save the changes incrementally (writes only the delta to the same stream).
            doc.Save();
        }

        // Copy the updated file to a new location if a separate output file is desired.
        File.Copy(inputPath, outputPath, true);
        Console.WriteLine($"Incrementally updated PDF saved to '{outputPath}'.");
    }
}