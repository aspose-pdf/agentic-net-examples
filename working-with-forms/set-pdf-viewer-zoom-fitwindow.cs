using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "output_form.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set the viewer to fit the window when the document is opened.
            // This effectively provides a zoomed‑in view for better usability.
            doc.FitWindow = true;

            // Optional: set additional viewer preferences (e.g., hide UI elements)
            // doc.HideToolBar = true;
            // doc.HideMenubar = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with FitWindow enabled: '{outputPath}'.");
    }
}