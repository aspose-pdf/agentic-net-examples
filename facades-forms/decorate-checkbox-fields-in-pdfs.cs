using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string sourceDir = @"C:\PdfSource";
        // Directory where decorated PDFs will be saved
        const string outputDir = @"C:\PdfDecorated";

        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory not found: {sourceDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the source directory
        foreach (string pdfPath in Directory.GetFiles(sourceDir, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // Load the PDF document via FormEditor (required for field decoration)
                using (FormEditor editor = new FormEditor())
                {
                    // Bind the source PDF
                    editor.BindPdf(pdfPath);

                    // Configure a consistent appearance for checkbox fields
                    // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                    editor.Facade.BorderColor = System.Drawing.Color.Blue;
                    editor.Facade.BackgroundColor = System.Drawing.Color.LightGray;
                    editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin; // float constant (1 point)
                    editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid; // int constant

                    // Apply the decoration to every checkbox field in the document
                    editor.DecorateField(FieldType.CheckBox);

                    // Save the decorated PDF to the output location
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Decorated PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
