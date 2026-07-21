using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a minimal PDF if it does not already exist.
        if (!System.IO.File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(inputPath);
            }
        }

        // Bind the PDF, read current viewer preferences, set HideMenubar flag, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Read existing viewer preferences (bitmask of ViewerPreference flags).
            int currentPreferences = editor.GetViewerPreference();
            Console.WriteLine($"Current viewer preferences: 0x{currentPreferences:X}");

            // Set the HideMenubar flag. This adds the flag to the existing preferences.
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

            // Verify that the flag is now set.
            int updatedPreferences = editor.GetViewerPreference();
            bool hideMenubarSet = (updatedPreferences & ViewerPreference.HideMenubar) != 0;
            Console.WriteLine($"HideMenubar flag set: {hideMenubarSet}");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
