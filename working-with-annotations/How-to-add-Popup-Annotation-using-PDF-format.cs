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

        // Load the PDF document (Document implements IDisposable, so use using)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the popup annotation will appear
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a PopupAnnotation on the specified page and rectangle
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                // Text displayed inside the popup window
                Contents = "This is a popup annotation.",
                // Border color of the annotation
                Color = Aspose.Pdf.Color.Yellow,
                // Set to false so the popup is closed when the document opens
                Open = false
            };

            // Add the popup annotation to the page's annotation collection
            page.Annotations.Add(popup);

            // Save the modified PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added and saved to '{outputPath}'.");
    }
}