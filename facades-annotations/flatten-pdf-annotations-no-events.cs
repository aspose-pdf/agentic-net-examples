using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
// Alias the nested FlattenSettings class to avoid ambiguity between Facades.Form and Forms.Form
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

        // Create an instance of FlattenSettings and set the desired options
        var settings = new FlattenSettings
        {
            CallEvents = false // disable calling events during flattening
        };

        // PdfAnnotationEditor implements IDisposable, so use a using block
        using (var editor = new PdfAnnotationEditor())
        {
            // Bind the PDF file to the editor
            editor.BindPdf(inputPath);

            // Flatten all annotations using the custom settings
            editor.FlatteningAnnotations(settings);

            // Save the flattened PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
    }
}
