using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
// Alias the nested FlattenSettings type to avoid ambiguity with Aspose.Pdf.Facades.Form
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

        // Create an instance of the flatten settings and configure it
        var settings = new FlattenSettings
        {
            CallEvents = false,   // Do not invoke form events while flattening
            HideButtons = true    // Hide form buttons after flattening
        };

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the annotation editor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Flatten all annotations using the configured settings
                editor.FlatteningAnnotations(settings);

                // Save the resulting PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
