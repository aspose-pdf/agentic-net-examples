using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_launch.pdf";
        const string externalFile = "example.txt"; // file to open when text is clicked

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the existing PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Use the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Add a visible text fragment to the page
                TextFragment tf = new TextFragment("Open external file");
                tf.Position = new Position(100, 700); // coordinates in points
                tf.TextState.FontSize = 14;
                tf.TextState.FontStyle = FontStyles.Bold;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                page.Paragraphs.Add(tf);

                // Define a rectangle that covers the text fragment.
                // Approximate width/height; adjust as needed.
                double llx = 100;               // lower‑left X
                double lly = 690;               // lower‑left Y (slightly below the baseline)
                double urx = 300;               // upper‑right X
                double ury = 720;               // upper‑right Y
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create a LaunchAction that opens the external file.
                // Use the constructor that accepts the file path (parameterless ctor is not available).
                LaunchAction launch = new LaunchAction(externalFile);

                // Create a LinkAnnotation with the rectangle and associate the launch action
                LinkAnnotation link = new LinkAnnotation(page, linkRect);
                link.Action = launch;
                // Optional visual appearance – Border requires the parent annotation in its ctor.
                link.Border = new Border(link) { Width = 0 };
                link.Color = Aspose.Pdf.Color.Transparent;

                // Add the annotation to the page
                page.Annotations.Add(link);

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF saved with launch action: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
