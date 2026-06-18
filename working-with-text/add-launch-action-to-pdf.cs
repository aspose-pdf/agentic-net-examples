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
        const string fileToLaunch = @"C:\Temp\example.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the clickable area (lower‑left x,y and upper‑right x,y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Add visible text inside the rectangle
            TextFragment tf = new TextFragment("Open file");
            tf.Position = new Position(110, 520); // Position the text within the rectangle
            page.Paragraphs.Add(tf);

            // Create a launch action that opens the external file
            LaunchAction launch = new LaunchAction(fileToLaunch);

            // Create a link annotation bound to the rectangle and assign the launch action
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Action = launch
            };

            // Optional: make the link border invisible
            link.Border = new Border(link) { Width = 0 };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with launch action saved to '{outputPath}'.");
    }
}