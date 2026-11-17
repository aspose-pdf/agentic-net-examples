using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document does not contain a page 4.");
                return;
            }

            // Get page 4
            Page page = doc.Pages[4];

            // Define the button rectangle (coordinates are in points; lower‑left to upper‑right)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);

            // Create a button form field
            ButtonField button = new ButtonField(page, buttonRect);
            button.PartialName = "AlertButton";
            // Set appearance (font, size, color) – required for the caption to render correctly
            button.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            // Set the visible label on the button
            button.NormalCaption = "Click Me";
            // Set background and border colours via Characteristics
            button.Characteristics.Background = System.Drawing.Color.LightGray;
            button.Characteristics.Border = System.Drawing.Color.Black;
            // Set border width and style – Border needs the parent annotation
            button.Border = new Border(button) { Width = 1, Style = BorderStyle.Solid };
            // Attach JavaScript that shows an alert when the button is activated
            button.OnActivated = new JavascriptAction("app.alert('Button clicked!');");

            // Add the button to the page's annotation collection
            page.Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: {outputPath}");
    }
}
