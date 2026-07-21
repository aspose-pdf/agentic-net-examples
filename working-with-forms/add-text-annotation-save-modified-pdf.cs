using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example modification: add a simple text annotation on the first page
            Page firstPage = pdfDoc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(firstPage, rect)
            {
                Title = "Note",
                Contents = "Modified PDF",
                Open = true,
                Color = Aspose.Pdf.Color.Yellow
            };
            firstPage.Annotations.Add(txtAnn);

            // Save to a new file name, preserving the original PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}