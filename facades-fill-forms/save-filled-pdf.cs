using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Add a simple text fragment to the first page
            Page page = doc.Pages[1];
            TextFragment tf = new TextFragment("Filled by Aspose.Pdf");
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.Position = new Position(100, 750);
            page.Paragraphs.Add(tf);

            // Save the modified document, preserving the original layout
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}