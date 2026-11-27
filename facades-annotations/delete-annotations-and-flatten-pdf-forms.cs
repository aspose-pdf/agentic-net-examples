using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
// Alias the nested FlattenSettings type to avoid ambiguity between Facades.Form and Forms.Form
using FlattenSettings = Aspose.Pdf.Forms.Form.FlattenSettings;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the annotation editor
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(doc);

                // Delete all annotations
                annotEditor.DeleteAnnotations();

                // Create a FlattenSettings instance and set CallEvents to false
                var flattenSettings = new FlattenSettings
                {
                    CallEvents = false
                };

                // Flatten annotations using the custom settings (no annotations remain, but the call satisfies the requirement)
                annotEditor.FlatteningAnnotations(flattenSettings);

                // Save any changes made by the editor (not strictly required here, but keeps the workflow consistent)
                annotEditor.Save(outputPath);
            }

            // Flatten remaining form fields with default settings
            doc.Form.Flatten();

            // Save the processed PDF (overwrites the file saved by the editor if it already exists)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
