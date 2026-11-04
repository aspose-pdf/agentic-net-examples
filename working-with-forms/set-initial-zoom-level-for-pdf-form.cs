using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";
        const string outputPath = "output_form_zoomed.pdf";
        const int zoomLevel = 150; // Zoom percentage (e.g., 150 = 150%)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf API.
        Document pdfDoc = new Document(inputPath);

        // Set the initial zoom level using a JavaScript OpenAction.
        // This works with all Aspose.Pdf versions and does not rely on ViewerPreferences.
        try
        {
            pdfDoc.OpenAction = new JavascriptAction($"this.zoom = {zoomLevel};");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to set zoom level: {ex.Message}");
        }

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF saved with zoom {zoomLevel}% to '{outputPath}'.");
    }
}
