using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
// Alias the correct FlattenSettings type to avoid ambiguity with Aspose.Pdf.Facades.Form
using FlattenSettings = Aspose.Pdf.Forms.Form.FlattenSettings;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Configure flatten settings as required
        var settings = new FlattenSettings
        {
            CallEvents        = false,
            HideButtons       = true,
            UpdateAppearances = true,
            // ApplyRedactions can be left false unless redaction flattening is needed
            ApplyRedactions   = false
        };

        // Use PdfAnnotationEditor to bind, flatten and save the PDF
        using (var editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            // Flatten annotations with the explicit settings
            editor.FlatteningAnnotations(settings);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
