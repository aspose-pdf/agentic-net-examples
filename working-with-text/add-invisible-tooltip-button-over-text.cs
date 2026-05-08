using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetText = "Hover over this text";
        const string tooltip    = "This is the tooltip shown on hover";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Find the target text on the first page (page indexing is 1‑based)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[1].Accept(absorber);

            // Locate the fragment that matches the target text
            TextFragment foundFragment = null;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.Text != null && fragment.Text.Contains(targetText))
                {
                    foundFragment = fragment;
                    break;
                }
            }

            if (foundFragment == null)
            {
                Console.Error.WriteLine("Target text not found.");
                return;
            }

            // Rectangle of the found text (coordinates are in PDF points)
            Aspose.Pdf.Rectangle textRect = foundFragment.Rectangle;

            // Create an invisible button field over the text rectangle
            ButtonField button = new ButtonField(doc, textRect);
            button.AlternateName = tooltip;                     // tooltip text (shown by PDF viewers)
            button.Color = Aspose.Pdf.Color.Transparent;       // make button invisible
            button.Border = new Border(button) { Width = 0 };   // no border

            // Optional: add a JavaScript action that shows an alert on mouse enter
            // (demonstrates use of PDF actions)
            button.Actions.OnEnter = new JavascriptAction($"app.alert('{tooltip}');");

            // Add the button to the document's form
            doc.Form.Add(button);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with invisible tooltip button: {outputPath}");
    }
}