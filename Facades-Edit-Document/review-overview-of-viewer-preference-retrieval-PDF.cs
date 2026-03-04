using System;
using System.IO;
using Aspose.Pdf.Facades;

class ViewerPreferenceDemo
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (which implements IDisposable) to bind the PDF
        // and retrieve its viewer preferences.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the PDF file to the editor facade
            editor.BindPdf(inputPdf);

            // Get the combined viewer preference flags as an integer
            int prefValue = editor.GetViewerPreference();

            // Output the raw integer value
            Console.WriteLine($"Viewer Preference value: {prefValue}");

            // Example: check specific ViewerPreference flags
            // ViewerPreference enum values are defined in Aspose.Pdf.Facades
            if ((prefValue & (int)ViewerPreference.HideMenubar) != 0)
                Console.WriteLine(" - Menubar is hidden.");

            if ((prefValue & (int)ViewerPreference.HideToolbar) != 0)
                Console.WriteLine(" - Toolbar is hidden.");

            if ((prefValue & (int)ViewerPreference.PageModeUseOutlines) != 0)
                Console.WriteLine(" - Page mode set to use outlines.");

            // If you need to save the PDF (even unchanged) you can do so.
            // This demonstrates the proper lifecycle: bind → operate → save → dispose.
            const string outputPdf = "output.pdf";
            editor.Save(outputPdf);
            Console.WriteLine($"PDF saved to '{outputPdf}'.");
        }
    }
}