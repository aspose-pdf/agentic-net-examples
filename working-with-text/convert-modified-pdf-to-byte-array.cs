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
            // Add a text annotation on the first page as an example change
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is an added annotation.",
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // Save the modified PDF into a byte array for further processing
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);               // Saves as PDF by default
                byte[] pdfBytes = ms.ToArray();

                // pdfBytes now contains the PDF data
                Console.WriteLine($"PDF saved to byte array, length = {pdfBytes.Length} bytes.");
            }
        }
    }
}