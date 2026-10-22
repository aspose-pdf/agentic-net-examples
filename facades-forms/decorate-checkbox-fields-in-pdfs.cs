using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // needed for FieldType enum

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to process
        const string inputDirectory = @"C:\PdfFolder";
        // Optional: output directory (can be same as input for in‑place update)
        const string outputDirectory = @"C:\PdfFolder\Processed";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Enumerate all PDF files in the directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Bind the document to the FormEditor facade
                    using (FormEditor editor = new FormEditor())
                    {
                        editor.BindPdf(doc);

                        // ----- Consistent decoration for checkbox fields -----
                        // The appearance is configured on the editor's Facade (FormFieldFacade)
                        editor.Facade.BorderColor = System.Drawing.Color.Blue;               // fully qualified
                        editor.Facade.BackgroundColor = System.Drawing.Color.LightGray;      // fully qualified
                        // Optional: set border width and style for a clearer look
                        editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin; // float constant
                        editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid; // int constant

                        // Apply the decoration to every checkbox field in the document
                        editor.DecorateField(FieldType.CheckBox);

                        // Save the modified PDF to the output location
                        string outputPath = Path.Combine(outputDirectory, Path.GetFileName(pdfPath));
                        editor.Save(outputPath);
                    }
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All PDFs have been processed.");
    }
}
