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

        // Prepare flatten settings: disable CallEvents and enable UpdateAppearances
        var settings = new FlattenSettings
        {
            CallEvents = false,
            UpdateAppearances = true
        };

        // Bind the PDF with PdfAnnotationEditor and flatten annotations using the settings
        using (var editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.FlatteningAnnotations(settings);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
