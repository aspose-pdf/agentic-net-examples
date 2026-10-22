using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_tooltip.pdf";
        const string searchText = "Target Text";          // text to locate
        const string tooltip = "This is a tooltip message";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Locate the first occurrence of the desired text using TextFragmentAbsorber
            // -----------------------------------------------------------------
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            absorber.TextSearchOptions = new TextSearchOptions(true); // case‑insensitive
            doc.Pages.Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine($"Text \"{searchText}\" not found.");
                return;
            }

            // Get the rectangle of the first match (1‑based page index)
            TextFragment fragment = absorber.TextFragments[0];
            int pageNumber = doc.Pages.IndexOf(fragment.Page) + 1; // Pages collection is 0‑based, PDF pages are 1‑based
            Aspose.Pdf.Rectangle textRect = fragment.Rectangle;

            // ---------------------------------------------------------------
            // Create an invisible ButtonField over the located text rectangle
            // ---------------------------------------------------------------
            ButtonField btn = new ButtonField(doc.Pages[pageNumber], textRect)
            {
                // No visible caption – keep the button transparent
                NormalCaption = string.Empty,
                // Tooltip (TU entry) – shown by many PDF viewers on hover
                AlternateName = tooltip
            };

            // Remove any border so the button is truly invisible
            btn.Border = new Border(btn) { Width = 0 };

            // Assign a JavaScript action that runs when the mouse enters the button area
            btn.Actions.OnEnter = new JavascriptAction($"app.alert('{tooltip}');");

            // Add the button to the document's form (AcroForm)
            doc.Form.Add(btn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip button: {outputPath}");
    }
}
