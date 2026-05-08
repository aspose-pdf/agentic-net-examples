using System;
using System.Runtime.Versioning;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // required for ViewerPreference enum

class Program
{
    // Suppress the platform‑specific warning that can be raised by Aspose internals
    #pragma warning disable CA1416 // Platform compatibility
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (a Facades class) to edit viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(inputPath);

            // Read current viewer preferences (returns an int)
            int currentPrefs = editor.GetViewerPreference();

            // Enable the HideMenubar flag while preserving other flags
            // Cast the enum value to int for the bitwise operation
            int newPrefs = currentPrefs | (int)ViewerPreference.HideMenubar;

            // Apply the modified preferences
            editor.ChangeViewerPreference(newPrefs);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences updated. Output saved to '{outputPath}'.");
    }
    #pragma warning restore CA1416 // Platform compatibility
}
