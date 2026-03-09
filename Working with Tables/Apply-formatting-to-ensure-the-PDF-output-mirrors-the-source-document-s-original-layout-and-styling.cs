using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "mirrored_output.pdf";

        // Verify that the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF document.
        // The using block guarantees deterministic disposal of the Document object.
        using (Document doc = new Document(inputPath))
        {
            // No modifications are performed here; the document is saved as‑is.
            // This preserves the original layout, fonts, colors, images and other styling.
            // If additional processing (e.g., flattening form fields) is required,
            // it can be invoked before the Save call, e.g.:
            // doc.Flatten();

            // Save the PDF. The parameterless Save method always writes a PDF,
            // regardless of the file extension, and retains the original appearance.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with original layout to '{outputPath}'.");
    }
}