using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "SquarePopup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Work with the first page (adjust index as needed)
            Page page = doc.Pages[1];

            // Define the rectangle for the square annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the square annotation
            SquareAnnotation square = new SquareAnnotation(page, squareRect);
            square.Color = Aspose.Pdf.Color.Red;
            // Optional: set border width
            square.Border = new Border(square) { Width = 2 };

            // Define the rectangle for the popup annotation (position of the note window)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(210, 500, 410, 650);

            // Create the popup annotation (the note)
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                // Title property is not available in older Aspose.Pdf versions; omit it.
                Contents = "This is a popup note attached to the square annotation.",
                Open = false // optional: start closed
            };

            // Link the popup to the square annotation
            square.Popup = popup;

            // Add both annotations to the page's annotation collection
            page.Annotations.Add(square);
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with Square and Popup annotations to '{outputPath}'.");
    }
}
