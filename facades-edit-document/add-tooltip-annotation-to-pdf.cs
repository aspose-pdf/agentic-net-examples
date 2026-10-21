// Program.cs
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tooltip_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation that works as a tooltip.
            // "Contents" is the text shown on hover, "Title" is optional.
            // Setting Open = false makes the annotation appear only when the mouse hovers.
            TextAnnotation tooltip = new TextAnnotation(doc.Pages[1], rect)
            {
                Contents = "This is additional information shown when the mouse hovers over the annotation.",
                Title    = "Info",
                Open     = false,
                // Optional visual styling (e.g., background color)
                Color    = Aspose.Pdf.Color.Yellow
            };

            // Add the annotation to the first page
            doc.Pages[1].Annotations.Add(tooltip);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tooltip annotation added. Output saved to '{outputPath}'.");
    }
}

// ---------------------------------------------------------------------------
// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// ---------------------------------------------------------------------------
// The project file references this source file, but its content is not required
// for the example. Providing an empty (or comment‑only) file satisfies the
// compiler and eliminates the CS2001 error.
// ---------------------------------------------------------------------------
// This file intentionally contains no executable code.
