using System;
using System.IO;
using Aspose.Pdf;               // for Document
using Aspose.Pdf.Facades;       // for FormEditor, FormFieldFacade
using Aspose.Pdf.Annotations;   // for AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document first – FormEditor expects a Document instance
        using (Document pdfDoc = new Document(inputPdf))
        using (FormEditor editor = new FormEditor(pdfDoc))
        {
            // Define visual attributes for the field via a facade
            editor.Facade = new FormFieldFacade();
            // Fully qualify the Color type to avoid ambiguity with Aspose.Pdf.Color
            editor.Facade.BackgroundColor = System.Drawing.Color.LightGreen;

            // Apply the visual changes to the field named "Status"
            editor.DecorateField("Status");

            // No annotation flags should be changed – pass 0 (no flags)
            editor.SetFieldAppearance("Status", (AnnotationFlags)0);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Field 'Status' background set to light green. Saved as {outputPdf}");
    }
}
