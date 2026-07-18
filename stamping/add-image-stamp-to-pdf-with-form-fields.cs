using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Annotations;        // For annotation types if needed (not used here)

class Program
{
    static void Main()
    {
        // Input PDF that contains form fields
        const string inputPdfPath  = "form_input.pdf";
        // Image to be used as a stamp (e.g., logo.png)
        const string stampImagePath = "logo.png";
        // Output PDF – form fields must remain interactive
        const string outputPdfPath = "form_with_stamp.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Position the stamp – here we place it 50 points from the left and bottom
                XIndent = 50,          // Horizontal coordinate (from left)
                YIndent = 50,          // Vertical coordinate (from bottom)
                // Optional: set size, opacity, alignment, etc.
                // Width = 100,        // Desired width (if scaling is needed)
                // Height = 50,        // Desired height
                Opacity = 0.8f,        // Slightly transparent so underlying fields stay visible
                Background = false     // Stamp appears on top of page content (default)
            };

            // Apply the stamp to each page that should show it.
            // Stamping does NOT flatten or alter form fields, so they stay functional.
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; fields remain interactive.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdfPath}'.");
    }
}