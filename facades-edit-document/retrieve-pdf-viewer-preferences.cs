using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Access ViewerPreferences via dynamic to stay compatible with older Aspose.Pdf versions
        dynamic viewerPrefs = null;
        try
        {
            viewerPrefs = ((dynamic)pdfDocument).ViewerPreferences;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            return;
        }

        // Output all viewer preferences (properties are accessed dynamically)
        Console.WriteLine("Viewer Preferences:");
        Console.WriteLine($"HideToolbar: {viewerPrefs.HideToolbar}");
        Console.WriteLine($"HideMenubar: {viewerPrefs.HideMenubar}");
        Console.WriteLine($"HideWindowUI: {viewerPrefs.HideWindowUI}");
        Console.WriteLine($"FitWindow: {viewerPrefs.FitWindow}");
        Console.WriteLine($"CenterWindow: {viewerPrefs.CenterWindow}");
        Console.WriteLine($"DisplayDocTitle: {viewerPrefs.DisplayDocTitle}");
        Console.WriteLine($"NonFullScreenPageMode: {viewerPrefs.NonFullScreenPageMode}");
        Console.WriteLine($"Direction: {viewerPrefs.Direction}");
        Console.WriteLine($"PrintScaling: {viewerPrefs.PrintScaling}");
        Console.WriteLine($"Duplex: {viewerPrefs.Duplex}");
        Console.WriteLine($"PickTrayByPDFSize: {viewerPrefs.PickTrayByPDFSize}");
        Console.WriteLine($"PrintPageRangeEnabled: {viewerPrefs.PrintPageRangeEnabled}");
        if (viewerPrefs.PrintPageRangeEnabled)
        {
            Console.WriteLine($"PrintPageRange: {viewerPrefs.PrintPageRange}");
        }
    }
}
