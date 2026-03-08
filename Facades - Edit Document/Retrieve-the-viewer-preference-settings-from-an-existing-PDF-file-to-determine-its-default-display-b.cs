using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file whose viewer preferences we want to read
        const string pdfPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfContentEditor provides methods to read (and modify) viewer preferences
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the PDF file to the editor
        editor.BindPdf(pdfPath);

        // Retrieve the viewer preference flags as an integer
        int prefFlags = editor.GetViewerPreference();

        // Output the raw integer value (for diagnostic purposes)
        Console.WriteLine($"Viewer Preference flags (int): {prefFlags}");

        // Example: check specific flags using the ViewerPreference enum members
        // The enum values are integral, so we cast them to int for the bitwise operation
        if ((prefFlags & (int)ViewerPreference.NonFullScreenPageModeUseOutlines) != 0)
        {
            Console.WriteLine("Viewer Preference: NonFullScreenPageModeUseOutlines is set.");
        }

        if ((prefFlags & (int)ViewerPreference.HideMenubar) != 0)
        {
            Console.WriteLine("Viewer Preference: HideMenubar is set.");
        }

        if ((prefFlags & (int)ViewerPreference.FitWindow) != 0)
        {
            Console.WriteLine("Viewer Preference: FitWindow is set.");
        }

        // Add additional flag checks as needed, using the members defined in ViewerPreference enum.
    }
}
