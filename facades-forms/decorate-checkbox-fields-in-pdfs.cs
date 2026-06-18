using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDir = "InputPdfs";
        // Output directory for the decorated PDFs
        const string outputDir = "OutputPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_decorated.pdf");

            // Use FormEditor to modify form fields
            using (FormEditor formEditor = new FormEditor())
            {
                // Bind the source PDF
                formEditor.BindPdf(pdfPath);

                // Configure visual appearance for the fields
                formEditor.Facade = new FormFieldFacade();
                formEditor.Facade.BackgroundColor = System.Drawing.Color.LightGray;   // Field background
                formEditor.Facade.BorderColor     = System.Drawing.Color.DarkBlue;    // Field border
                formEditor.Facade.TextColor       = System.Drawing.Color.Black;       // Text (check mark) color
                formEditor.Facade.Alignment       = FormFieldFacade.AlignCenter;      // Center alignment

                // Apply the appearance to all checkbox fields
                formEditor.DecorateField(Aspose.Pdf.Facades.FieldType.CheckBox);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }

            Console.WriteLine($"Decorated PDF saved to: {outputPath}");
        }
    }
}