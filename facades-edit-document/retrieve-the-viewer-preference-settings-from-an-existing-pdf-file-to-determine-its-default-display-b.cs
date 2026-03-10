using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the PDF file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the facade that works with PDF content
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF into the facade
        editor.BindPdf(inputPath);

        // Retrieve the viewer preference flags (as an integer)
        int prefValue = editor.GetViewerPreference();

        // Example checks for specific viewer preferences using the correct ViewerPreference constants
        if ((prefValue & (int)ViewerPreference.PageModeUseOutlines) != 0)
        {
            Console.WriteLine("Viewer Preference: Page mode uses outline.");
        }

        if ((prefValue & (int)ViewerPreference.HideMenubar) != 0)
        {
            Console.WriteLine("Viewer Preference: Hide menu bar.");
        }

        // Output the raw integer value representing all flags
        Console.WriteLine($"Viewer Preference Flags Value: {prefValue}");
    }
}
