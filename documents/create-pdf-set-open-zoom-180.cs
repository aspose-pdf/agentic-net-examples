using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // required for GoToAction and related destination types

class Program
{
    static void Main()
    {
        const string outputPath = "Zoomed1_8.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Insert a TextFragment onto the page
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");
            page.Paragraphs.Add(tf);

            // Set the document's open action to zoom to 180% (1.8)
            // Use the GoToAction constructor that accepts an ExplicitDestinationType
            doc.OpenAction = new GoToAction(new XYZExplicitDestination(page, 0, 0, 1.8f));

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
