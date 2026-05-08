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
        const string outputPath = "output_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the annotation will appear (coordinates in points)
            // Fully qualified type avoids ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a LinkAnnotation – this type supports the Action property
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], rect)
            {
                // Optional visual settings
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Click to run JavaScript"
            };

            // Attach a JavaScript action to the annotation
            link.Action = new JavascriptAction("app.alert('Hello from JavaScript!');");

            // Add the annotation to the first page
            doc.Pages[1].Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript annotation to '{outputPath}'.");
    }
}