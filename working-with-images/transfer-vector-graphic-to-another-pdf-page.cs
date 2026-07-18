using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";      // PDF containing the original vector graphic
        const string outputPath = "output.pdf";      // Resulting PDF with the graphic transferred

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Load the PDF, extract vector graphics from page 1 and place them on page 2
        using (Document doc = new Document(sourcePath))
        {
            // Absorb vector graphics from the source page (page index is 1‑based)
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(doc.Pages[1]);

            // Ensure there is at least one graphic element to transfer
            if (absorber.Elements.Count == 0)
            {
                Console.WriteLine("No vector graphics found on page 1.");
            }
            else
            {
                // Destination page (page 2). If the document has only one page, add a new blank page.
                Page destinationPage;
                if (doc.Pages.Count >= 2)
                {
                    destinationPage = doc.Pages[2];
                }
                else
                {
                    destinationPage = doc.Pages.Add();
                }

                // Transfer each extracted graphic element
                foreach (GraphicElement element in absorber.Elements)
                {
                    // Adjust the position using the Position property.
                    // Example: shift the element 50 points right and 100 points up.
                    element.Position = new Point(element.Position.X + 50, element.Position.Y + 100);

                    // Add the element to the destination page.
                    element.AddOnPage(destinationPage);
                }

                Console.WriteLine($"Transferred {absorber.Elements.Count} vector graphic(s) to page {destinationPage.Number}.");
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Output saved to '{outputPath}'.");
    }
}
