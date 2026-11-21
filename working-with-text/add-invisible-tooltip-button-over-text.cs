using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color used in field characteristics
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction, AnnotationFlags
using Aspose.Pdf.Forms; // ButtonField, Border
using Aspose.Pdf.Text; // TextFragmentAbsorber, TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_tooltip.pdf";
        const string searchText = "TargetText";          // text to overlay
        const string tooltip = "This is a tooltip";      // tooltip message

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the desired text fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages.Accept(absorber);

            // Iterate over each found fragment and place an invisible button over it
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Get the rectangle of the fragment (coordinates are in points)
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create a button field on the same page as the fragment
                ButtonField button = new ButtonField(fragment.Page, rect)
                {
                    // Unique name for the field
                    PartialName = $"tooltipBtn_{fragment.Page.Number}_{Guid.NewGuid()}",
                    // Prevent user interaction (read‑only)
                    ReadOnly = true,
                    // Hide the widget (no visual representation)
                    Flags = AnnotationFlags.Invisible
                };

                // Make the button background transparent via field characteristics
                button.Characteristics.Background = System.Drawing.Color.Transparent;

                // Remove any visible border
                button.Border = new Border(button) { Width = 0 };

                // Add a JavaScript action that shows a tooltip when the mouse enters the button area
                JavascriptAction jsAction = new JavascriptAction($"app.alert('{tooltip}');");
                button.Actions.OnEnter = jsAction; // Mouse‑enter action

                // Add the button to the document's form collection
                doc.Form.Add(button);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with invisible tooltip buttons: {outputPath}");
    }
}
