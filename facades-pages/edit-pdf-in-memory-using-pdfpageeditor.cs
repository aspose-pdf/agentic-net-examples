using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and modify it inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a simple text annotation on the first page
            Page page = doc.Pages[1];
            var rect = new Rectangle(100, 500, 300, 550);
            var annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This PDF was processed in memory.",
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // Save the modified PDF directly to a MemoryStream (no file I/O)
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream, SaveFormat.Pdf);
                pdfStream.Position = 0; // reset for reading

                Console.WriteLine($"Modified PDF size in memory: {pdfStream.Length} bytes");

                // At this point pdfStream can be passed to other APIs (e.g., HTTP response, further processing)
            }
        }
    }
}
