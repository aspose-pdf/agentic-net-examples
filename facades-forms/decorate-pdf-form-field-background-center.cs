using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF containing a form field named "Header"
        const string outputPdfPath = "output.pdf";     // destination PDF
        const string headerFieldName = "Header";       // exact name of the field to decorate
        const string backgroundImagePath = "header_bg.jpg"; // image to use as background (illustrative)

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Use FormEditor (facade) to modify the appearance of the form field.
        // The facade must be disposed; wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPdfPath);

            // Create a FormFieldFacade to specify visual attributes.
            formEditor.Facade = new FormFieldFacade();

            // Center the text inside the field.
            formEditor.Facade.Alignment = FormFieldFacade.AlignCenter;

            // NOTE: FormFieldFacade does not expose a direct property for a background image.
            // As a practical workaround, we set a background color (light gray) to illustrate
            // that a visual background can be applied. If an actual image background is required,
            // a different API (e.g., HeaderArtifact) would be needed.
            formEditor.Facade.BackgroundColor = System.Drawing.Color.LightGray;

            // Apply the visual settings to the specific field named "Header".
            formEditor.DecorateField(headerFieldName);

            // Save the modified PDF.
            formEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Field '{headerFieldName}' has been decorated and saved to '{outputPdfPath}'.");
    }
}