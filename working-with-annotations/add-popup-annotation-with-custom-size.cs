using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        // Load existing PDF if it exists; otherwise create a new blank document
        using (Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Ensure the document has at least one page (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            Page page = doc.Pages[1];

            // ----- Parent markup annotation (TextAnnotation) -----
            // Define custom rectangle for the parent annotation
            Aspose.Pdf.Rectangle parentRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            TextAnnotation parent = new TextAnnotation(page, parentRect)
            {
                Title = "Note",
                Contents = "Parent annotation",
                Open = false,                     // initially closed
                Icon = TextIcon.Note,
                Color = Aspose.Pdf.Color.Yellow   // annotation border color
            };
            page.Annotations.Add(parent);

            // ----- Popup annotation attached to the parent -----
            // Define custom rectangle for the popup (size and position)
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(210, 610, 350, 720);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "This is a popup annotation attached to the parent.",
                Open = true,                       // show popup open by default
                Color = Aspose.Pdf.Color.LightGray
            };
            // Associate the popup with its parent markup annotation
            popup.Parent = parent;

            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with popup annotation saved to '" + outputPath + "'.");
    }
}