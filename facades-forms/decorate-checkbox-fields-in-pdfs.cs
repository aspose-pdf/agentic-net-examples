using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the PDF files
        const string inputDirectory = @"C:\PdfFolder";
        // Output directory for the decorated PDFs
        const string outputDirectory = @"C:\PdfFolder\Decorated";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{fileName}_decorated.pdf");

            try
            {
                // Initialize FormEditor with source and destination files
                using (FormEditor editor = new FormEditor(inputPath, outputPath))
                {
                    // Set visual attributes for the fields
                    editor.Facade = new FormFieldFacade();
                    editor.Facade.BackgroundColor = Color.LightGray;   // Background of the checkbox
                    editor.Facade.TextColor       = Color.DarkBlue;   // Check mark color
                    editor.Facade.BorderColor     = Color.Black;     // Border color
                    editor.Facade.Alignment       = FormFieldFacade.AlignCenter;

                    // Apply the decoration to all checkbox fields
                    editor.DecorateField(FieldType.CheckBox);

                    // Save the modified PDF
                    editor.Save();
                }

                Console.WriteLine($"Decorated PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}