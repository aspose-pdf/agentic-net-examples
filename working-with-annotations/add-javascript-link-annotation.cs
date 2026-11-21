using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string url        = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Use the first page; adjust index as needed
            Page page = doc.Pages[1];

            // Define the clickable area (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation on the page
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // JavaScript that opens a new browser window with the specified URL
            string js = $"app.launchURL('{url}', true);";

            // Assign the JavaScript action to the annotation
            link.Action = new JavascriptAction(js);

            // Optional visual styling
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added and saved to '{outputPath}'.");
    }
}