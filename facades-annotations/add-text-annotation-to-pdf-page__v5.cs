using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document using the high‑level API
        Document pdf = new Document(inputPath);

        // Define the annotation rectangle (coordinates in points)
        double llx = 100; // lower‑left X
        double lly = 500; // lower‑left Y
        double urx = 300; // upper‑right X
        double ury = 550; // upper‑right Y

        // Create a TextAnnotation with title "Comment" and contents "Comment"
        TextAnnotation textAnnot = new TextAnnotation(pdf.Pages[2], new Rectangle(llx, lly, urx, ury))
        {
            Title = "Comment",
            Contents = "Comment"
        };

        // Add the annotation to page 2
        pdf.Pages[2].Annotations.Add(textAnnot);

        // Save the modified PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}
