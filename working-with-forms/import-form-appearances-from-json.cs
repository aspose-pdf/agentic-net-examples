using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, JSON appearance file, and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string appearanceJsonPath = "appearance.json";
        const string outputPdfPath = "output.pdf";

        // Verify that the source files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(appearanceJsonPath))
        {
            Console.Error.WriteLine($"Appearance JSON not found: {appearanceJsonPath}");
            return;
        }

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document pdfDoc = new Document(sourcePdfPath))
            {
                // Import form field definitions and appearance settings from the JSON file
                // This method updates existing fields and adds missing ones as needed.
                pdfDoc.Form.ImportFromJson(appearanceJsonPath);

                // Save the modified PDF with the imported appearance settings
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF saved with imported appearances: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}