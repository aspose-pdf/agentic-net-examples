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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with read/write access to enable incremental updates
        using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document document = new Document(fileStream))
        {
            // Modify the document – add a text annotation on the first page
            Page page = document.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title = "Note";
            annotation.Contents = "Incremental update example.";
            annotation.Open = true;
            page.Annotations.Add(annotation);

            // Save incrementally (writes changes to the same file without rewriting the whole PDF)
            document.Save();
        }

        // Copy the updated file to a new name for demonstration purposes
        File.Copy(inputPath, outputPath, true);
        Console.WriteLine($"Incrementally updated PDF saved as '{outputPath}'.");
    }
}