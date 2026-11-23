using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "styled_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            var tagged = doc.TaggedContent;

            // Use dynamic to avoid compile‑time dependency on concrete tagged‑content classes
            dynamic root = tagged.RootElement;

            // Create a new paragraph element
            dynamic para = tagged.CreateParagraphElement();

            // Create a span element that will hold the actual text and styling
            dynamic span = tagged.CreateSpanElement();
            span.SetText("Styled paragraph text.");

            // Apply font styling via StructureTextState on the span
            dynamic txtState = span.TextState;
            txtState.Font = FontRepository.FindFont("Helvetica");               // Font
            txtState.FontSize = 14;                                            // FontSize
            txtState.FontStyle = FontStyles.Bold | FontStyles.Italic;          // FontStyle
            txtState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.2, 0.0, 0.6); // ForegroundColor (dark purple)

            // Attach the span to the paragraph, then the paragraph to the root
            para.AppendChild(span);
            root.AppendChild(para);

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
