using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input OFD file and output PDF file paths
        const string inputOfdPath  = "input.ofd";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfdPath}");
            return;
        }

        // Load the OFD document using the appropriate load options
        using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Choose the page where the link annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Set a visible border color (optional)
                Color = Aspose.Pdf.Color.Blue,
                // Set the action to open an external URL
                Action = new GoToURIAction("https://www.example.com")
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified document as PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Link annotation added and saved to '{outputPdfPath}'.");
    }
}