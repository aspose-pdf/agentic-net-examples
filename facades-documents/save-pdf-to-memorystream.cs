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

        using (Document document = new Document(inputPath))
        {
            Page page = document.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title = "Note";
            annotation.Contents = "Added via MemoryStream example.";
            annotation.Open = true;
            annotation.Icon = TextIcon.Note;
            page.Annotations.Add(annotation);

            using (MemoryStream memory = new MemoryStream())
            {
                document.Save(memory);
                memory.Position = 0;
                Console.WriteLine($"PDF saved to memory stream, length = {memory.Length} bytes.");
                // The memory stream can now be used further (e.g., returned, sent over network, etc.)
            }
        }
    }
}