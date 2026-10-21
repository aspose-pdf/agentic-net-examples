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

        // Load the PDF and apply a simple modification
        using (Document doc = new Document(inputPath))
        {
            // Add a text annotation on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Modified by Aspose.Pdf example",
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // Export the modified PDF to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Explicitly specify the format for clarity (optional)
                // The null‑forgiving operator (!) silences CS8600 warnings about possible null values.
                doc.Save(ms!, SaveFormat.Pdf);
                ms.Position = 0; // reset for downstream processing

                // Example output – the stream can now be passed to other components
                Console.WriteLine($"PDF exported to memory stream, length = {ms.Length} bytes");
            }
        }
    }
}
