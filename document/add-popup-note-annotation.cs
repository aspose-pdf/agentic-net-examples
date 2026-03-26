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

        using (Document doc = new Document(inputPath))
        {
            // Use the first page of the document
            Page page = doc.Pages[1];

            // Rectangle for the sticky‑note icon (coordinates are in points)
            Aspose.Pdf.Rectangle noteRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a TextAnnotation (the visible sticky note)
            TextAnnotation textAnn = new TextAnnotation(page, noteRect)
            {
                Title = "Note",
                Contents = "Hover to see details",
                Icon = TextIcon.Note,
                Color = Aspose.Pdf.Color.Yellow
            };

            // Create a PopupAnnotation that holds the detailed information
            PopupAnnotation popup = new PopupAnnotation(page, noteRect)
            {
                Contents = "This is the detailed information shown in the pop‑up window.",
                Open = false,
                Color = Aspose.Pdf.Color.LightGray
            };

            // Associate the popup with the sticky note
            textAnn.Popup = popup;

            // Add the annotation (the popup is linked via the TextAnnotation)
            page.Annotations.Add(textAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pop‑up note annotation added to '{outputPath}'.");
    }
}