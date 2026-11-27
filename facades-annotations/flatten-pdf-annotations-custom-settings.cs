using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
// Alias the nested FlattenSettings type to avoid ambiguity between Facades.Form and Forms.Form
using FlattenSettings = Aspose.Pdf.Forms.Form.FlattenSettings;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the document to the annotation editor (facade API)
            editor.BindPdf(doc);

            // Create flatten settings instance and configure it
            var settings = new FlattenSettings
            {
                CallEvents = false,
                ApplyRedactions = true,
                UpdateAppearances = true,
                // HideButtons can be set if needed; leaving default (false)
            };

            // Flatten all annotations using the configured settings
            editor.FlatteningAnnotations(settings);

            // Save the flattened PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
