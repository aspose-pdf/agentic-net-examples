using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "DescriptionJustified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor works with form fields (no need to instantiate Form separately)
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document
            formEditor.BindPdf(inputPath);

            // Set horizontal alignment of the "Description" field to Justify
            formEditor.SetFieldAlignment("Description", (int)HorizontalAlignment.Justify);

            // Prepare visual decoration via Facade (border + background)
            formEditor.Facade = new FormFieldFacade
            {
                BorderColor = System.Drawing.Color.Black,
                BorderWidth = 1,
                BackgroundColor = System.Drawing.Color.LightGray
            };

            // Apply the decoration to the existing field
            formEditor.DecorateField("Description");

            // Save the modified PDF
            formEditor.Save(outputPath);
            Console.WriteLine($"Justified PDF saved to '{outputPath}'.");
        }
    }
}
