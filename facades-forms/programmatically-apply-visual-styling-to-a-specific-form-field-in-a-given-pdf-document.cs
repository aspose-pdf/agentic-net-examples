using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // kept for other potential drawing needs

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string sourcePdf = "input.pdf";
        const string outputPdf = "styled_output.pdf";

        // Fully qualified name of the form field to style
        const string targetField = "myTextField";

        // Verify that the source file exists
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Load the PDF document (FormEditor expects a Document instance)
        Document pdfDoc = new Document(sourcePdf);

        // Use the non‑obsolete FormEditor constructor that accepts a Document
        using (FormEditor editor = new FormEditor(pdfDoc))
        {
            // Initialise the visual‑style facade
            editor.Facade = new FormFieldFacade();

            // Set desired visual attributes – fully qualify System.Drawing.Color to avoid ambiguity
            editor.Facade.BackgroundColor = System.Drawing.Color.Red;
            editor.Facade.TextColor       = System.Drawing.Color.Blue;
            editor.Facade.BorderColor     = System.Drawing.Color.Green;
            editor.Facade.Alignment       = FormFieldFacade.AlignCenter;

            // Apply the styling to the specific field
            editor.DecorateField(targetField);

            // Save the changes directly to the output file (new recommended API)
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPdf}'.");
    }
}
