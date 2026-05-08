using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Color for visual attributes
using Aspose.Pdf;                         // Document
using Aspose.Pdf.Facades;                 // FormEditor, FormFieldFacade, FieldType

class Program
{
    static void Main()
    {
        // Input directory containing PDF files to be processed
        // Use paths that are valid on the current OS. Here we build them relative to the current working directory.
        string baseDir   = Directory.GetCurrentDirectory();
        string inputDir  = Path.Combine(baseDir, "Pdf", "Input");
        string outputDir = Path.Combine(baseDir, "Pdf", "Output");

        // Ensure the directories exist
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            // Determine the corresponding output file path
            string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));

            // Load the PDF document first – FormEditor now expects a Document instance, not a file path.
            using (Document pdfDoc = new Document(inputPath))
            using (FormEditor formEditor = new FormEditor(pdfDoc))
            {
                // Configure visual appearance for the fields
                formEditor.Facade = new FormFieldFacade();
                formEditor.Facade.BackgroundColor = System.Drawing.Color.LightGray;   // Field background
                formEditor.Facade.TextColor       = System.Drawing.Color.Black;      // Check mark color
                formEditor.Facade.BorderColor     = System.Drawing.Color.DarkBlue;   // Border color
                formEditor.Facade.Alignment       = FormFieldFacade.AlignCenter;

                // Apply the appearance to all checkbox fields in the document
                formEditor.DecorateField(FieldType.CheckBox);

                // Persist the changes to the output file
                formEditor.Save(outputPath);
            }

            Console.WriteLine($"Decorated PDF saved to: {outputPath}");
        }
    }
}
