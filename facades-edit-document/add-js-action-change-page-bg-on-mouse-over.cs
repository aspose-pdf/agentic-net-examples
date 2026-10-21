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

        // Load the PDF document
        Document doc = new Document(inputPath);
        Page firstPage = doc.Pages[1];

        // Define the annotation rectangle (coordinates are in points)
        Rectangle annRect = new Rectangle(100, 500, 300, 550);

        // Create a link annotation (invisible) and attach a JavaScript action
        LinkAnnotation link = new LinkAnnotation(firstPage, annRect);
        // JavaScript to change the page background color when the mouse enters the annotation
        link.Action = new JavascriptAction("this.bgColor = color.yellow;");
        // Make the annotation invisible
        link.Color = Color.Transparent;

        // Add the annotation to the first page
        firstPage.Annotations.Add(link);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF with JavaScript annotation saved to '{outputPath}'.");
    }
}
