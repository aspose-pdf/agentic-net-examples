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

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Attempt to retrieve ViewerPreferences. The property exists only in newer
        // versions of Aspose.Pdf, so we use dynamic access and handle the possible
        // RuntimeBinderException for older library versions.
        try
        {
            dynamic doc = pdfDocument; // bypass compile‑time member check
            var prefs = doc.ViewerPreferences; // may throw at runtime if not present

            Console.WriteLine("Viewer Preferences:");
            Console.WriteLine($"HideToolbar       : {prefs.HideToolbar}");
            Console.WriteLine($"HideMenubar       : {prefs.HideMenubar}");
            Console.WriteLine($"HideWindowUI      : {prefs.HideWindowUI}");
            Console.WriteLine($"FitWindow         : {prefs.FitWindow}");
            Console.WriteLine($"CenterWindow      : {prefs.CenterWindow}");
            Console.WriteLine($"DisplayDocTitle   : {prefs.DisplayDocTitle}");
            Console.WriteLine($"NonFullScreenPageMode : {prefs.NonFullScreenPageMode}");
            Console.WriteLine($"Direction         : {prefs.Direction}");
            Console.WriteLine($"PrintScaling      : {prefs.PrintScaling}");
            Console.WriteLine($"Duplex            : {prefs.Duplex}");
            Console.WriteLine($"PickTrayByPDFSize : {prefs.PickTrayByPDFSize}");
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }
    }
}
