using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a square (rectangle) annotation on the first page
            // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            SquareAnnotation square = new SquareAnnotation(doc.Pages[1], rect);
            square.Color = Aspose.Pdf.Color.Yellow;          // visual appearance
            square.Contents = "Click to see an alert";       // tooltip text

            // NOTE:
            // The current Aspose.Pdf version referenced in this project does not expose
            // the 'Action' property on SquareAnnotation nor the JavaScriptAction class.
            // Therefore a JavaScript action cannot be attached directly.
            // To enable JavaScript actions, upgrade to a newer Aspose.Pdf version that
            // provides SquareAnnotation.Action = new JavaScriptAction("app.alert('Hello');");
            // For now we add the annotation without an action.

            // Add the annotation to the page's Annotations collection
            doc.Pages[1].Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rectangle annotation: '{outputPath}'");
    }
}