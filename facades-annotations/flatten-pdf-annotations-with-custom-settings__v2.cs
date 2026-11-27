using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
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

        try
        {
            // Load the PDF document
            using (var doc = new Document(inputPath))
            {
                // Bind the document to the annotation editor
                using (var annotEditor = new PdfAnnotationEditor())
                {
                    annotEditor.BindPdf(doc);

                    // Create flatten settings: disable call events, apply appearance and redaction
                    var flattenSettings = new FlattenSettings
                    {
                        CallEvents = false,
                        UpdateAppearances = true,
                        ApplyRedactions = true
                    };

                    // Flatten annotations with the specified settings
                    annotEditor.FlatteningAnnotations(flattenSettings);

                    // Save the resulting PDF
                    annotEditor.Save(outputPath);
                }
            }

            Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
