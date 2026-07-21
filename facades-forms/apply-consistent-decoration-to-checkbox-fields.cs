using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputDirectory  = @"C:\Pdf\Input";
        // Directory where decorated PDFs will be saved
        const string outputDirectory = @"C:\Pdf\Output";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{fileName}_decorated.pdf");

            // FormEditor works with an input PDF and writes to the specified output PDF
            using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
            {
                // Set visual attributes that will be applied to the fields
                formEditor.Facade = new FormFieldFacade();
                formEditor.Facade.BackgroundColor = System.Drawing.Color.LightGray;
                formEditor.Facade.TextColor       = System.Drawing.Color.Black;
                formEditor.Facade.BorderColor     = System.Drawing.Color.DarkBlue;
                formEditor.Facade.Alignment       = FormFieldFacade.AlignCenter;

                // Apply the visual attributes to all checkbox fields in the document
                formEditor.DecorateField(FieldType.CheckBox);

                // The changes are saved when the FormEditor is disposed (or you could call Save explicitly)
            }

            Console.WriteLine($"Decorated PDF saved to: {outputPath}");
        }
    }
}