using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

        // Facades class usage (no disposal required)
        PdfFileEditor editor = new PdfFileEditor();

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a text annotation to the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Sample annotation",
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // Save the modified PDF to a MemoryStream for further processing
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0; // Reset stream position for downstream consumers

                // Example: output the size of the generated PDF
                Console.WriteLine($"PDF saved to MemoryStream, size = {ms.Length} bytes");
                // The MemoryStream can now be passed to other APIs without touching the file system
            }
        }
    }
}