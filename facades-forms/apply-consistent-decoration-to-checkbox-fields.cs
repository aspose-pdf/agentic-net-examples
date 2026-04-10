using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to process
        const string inputFolder = @"C:\PdfInput";
        // Directory where the decorated PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_decorated.pdf");

            try
            {
                // Bind the PDF to a FormEditor instance
                using (FormEditor editor = new FormEditor())
                {
                    editor.BindPdf(pdfPath);

                    // Configure visual appearance for the fields
                    editor.Facade = new FormFieldFacade
                    {
                        BackgroundColor = Color.LightYellow,
                        TextColor       = Color.DarkBlue,
                        BorderColor     = Color.Gray,
                        Alignment       = FormFieldFacade.AlignCenter
                    };

                    // Apply the appearance to all checkbox fields in the document
                    editor.DecorateField(FieldType.CheckBox);

                    // Save the modified PDF
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All files processed.");
    }
}