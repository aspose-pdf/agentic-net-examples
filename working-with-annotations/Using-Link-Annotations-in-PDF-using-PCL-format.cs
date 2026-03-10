using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PCL file and output PDF file paths
        const string pclPath   = "input.pcl";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        // Load the PCL file into a PDF document using PclLoadOptions
        PclLoadOptions pclLoadOptions = new PclLoadOptions();
        using (Document doc = new Document(pclPath, pclLoadOptions))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Loaded document contains no pages.");
                return;
            }

            // Create a link annotation on the first page
            // Define the rectangle where the link will appear (coordinates are in points)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // The annotation is placed on page 1
            Page page = doc.Pages[1];
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Optional visual appearance
                Color = Aspose.Pdf.Color.Blue,
                // Tooltip text shown when hovering
                Contents = "Go to page 2"
            };

            // Define the action: navigate to page 2 (fit to window)
            // Use GoToAction with the target page
            link.Action = new GoToAction(doc.Pages[2]);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{pdfPath}'.");
    }
}