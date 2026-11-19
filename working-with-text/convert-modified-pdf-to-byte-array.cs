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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a text annotation on the first page
            Page page = doc.Pages[1]; // 1‑based page indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Sample annotation",
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // Save the modified PDF to a byte array
            byte[] pdfBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms); // Saves as PDF to the stream
                pdfBytes = ms.ToArray();
            }

            // pdfBytes now holds the PDF data for further processing or transmission
            Console.WriteLine($"PDF saved to byte array, length = {pdfBytes.Length} bytes.");
        }
    }
}