using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Add a text annotation on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100.0, 500.0, 300.0, 550.0);
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title = "Note";
            annotation.Contents = "Sample annotation added.";
            annotation.Color = Aspose.Pdf.Color.Yellow;
            page.Annotations.Add(annotation);

            // Export the modified PDF to a memory stream
            using (MemoryStream outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                // Reset the stream position for downstream processing
                outputStream.Position = 0;

                Console.WriteLine($"Exported PDF size: {outputStream.Length} bytes");
                // Further processing can be performed using outputStream
            }
        }
    }
}