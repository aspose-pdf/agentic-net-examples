using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for GoToURIAction
using Aspose.Pdf.Text;   // for Aspose.Pdf.Rectangle (if needed)

class Program
{
    static void Main()
    {
        // Input PCL file path
        const string pclPath = "input.pcl";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load the PCL file into a PDF Document using PclLoadOptions
        using (Document doc = new Document(pclPath, new PclLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Define the rectangle where the link annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the first page
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], linkRect)
            {
                // Optional visual properties
                Color = Aspose.Pdf.Color.Blue,
                // Set the action to open an external URL
                Action = new GoToURIAction("https://www.example.com")
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(link);

            // Save the modified document as PDF (PCL cannot be used as an output format)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Link annotation added and PDF saved to '{pdfPath}'.");
    }
}