using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume we work with the first page
            Page page = doc.Pages[1];

            // Define rectangle for the RichMedia annotation
            Aspose.Pdf.Rectangle richMediaRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            // Create a RichMedia annotation (placeholder – actual media not set)
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, richMediaRect)
            {
                // Optional: give it a name to reference later if needed
                Name = "MyRichMedia"
            };
            page.Annotations.Add(richMedia);

            // Define rectangle for the button annotation
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(350, 500, 450, 550);
            // Create a push button field
            ButtonField button = new ButtonField(page, buttonRect)
            {
                // Visual appearance
                Color = Aspose.Pdf.Color.LightGray,
                Contents = "Toggle Media"
            };

            // When the button is pressed, hide the RichMedia annotation
            button.Actions.OnPressMouseBtn = new HideAction(richMedia, true);
            // When the button is released, show the RichMedia annotation
            button.Actions.OnReleaseMouseBtn = new HideAction(richMedia, false);

            // Add the button to the page
            page.Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}