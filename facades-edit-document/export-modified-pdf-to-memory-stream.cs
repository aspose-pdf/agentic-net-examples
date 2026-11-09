using System;
using System.IO;
using Aspose.Pdf;
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

        // Facade usage example – instantiate a PdfFileEditor (no IDisposable).
        PdfFileEditor editor = new PdfFileEditor();

        // Load and modify the PDF using the core Document API.
        using (Document doc = new Document(inputPath))
        {
            // Sample modification: add a text annotation to the first page.
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            Aspose.Pdf.Annotations.TextAnnotation annotation = new Aspose.Pdf.Annotations.TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "Sample annotation",
                Open = true,
                Icon = Aspose.Pdf.Annotations.TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // Export the modified PDF to a memory stream.
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0; // Reset for downstream processing.

                // Pass the stream to further processing logic.
                ProcessPdfStream(ms);
            }
        }
    }

    static void ProcessPdfStream(Stream pdfStream)
    {
        // Placeholder for additional handling of the PDF stream.
        Console.WriteLine($"PDF stream length: {pdfStream.Length} bytes");
    }
}