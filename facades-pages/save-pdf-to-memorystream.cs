using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Document document = new Document(inputPath))
        {
            // Add a simple text annotation on the first page
            Page page = document.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title = "Note";
            annotation.Contents = "Sample annotation";
            annotation.Color = Aspose.Pdf.Color.Yellow;
            page.Annotations.Add(annotation);

            // Save the modified PDF into a MemoryStream
            using (MemoryStream memory = new MemoryStream())
            {
                document.Save(memory);
                memory.Position = 0; // reset for further processing
                Console.WriteLine($"PDF saved to memory stream, length = {memory.Length} bytes");
                // Further processing can be performed here using the stream
            }
        }
    }
}