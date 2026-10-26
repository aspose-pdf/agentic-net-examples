using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_link.pdf";
        const string url = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the clickable area for the link annotation
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a JavaScript action that opens a new browser window with the URL
            // The JavaScript uses the Acrobat JavaScript API: app.launchURL(url, true)
            JavascriptAction jsAction = new JavascriptAction($"app.launchURL('{url}', true);");

            // Create the link annotation and assign the rectangle and the JavaScript action
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Action = jsAction,
                // Optional: make the annotation invisible (transparent color)
                Color = Color.Transparent
            };

            // Border must be instantiated with the parent annotation instance.
            // It cannot be set via an object‑initializer because the annotation variable
            // is not assigned yet. Set it after the annotation is created.
            link.Border = new Border(link)
            {
                Width = 0,               // 0 = invisible border
                Style = BorderStyle.Solid
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript link saved to '{outputPath}'.");
    }
}
