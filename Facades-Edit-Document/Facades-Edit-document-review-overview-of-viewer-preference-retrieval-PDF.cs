using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main(string[] args)
    {
        // Generic input file name – replace with your PDF file as needed
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // ViewerPreferences property is available only in newer Aspose.Pdf versions.
            // Use dynamic access to avoid compile‑time errors on older versions.
            dynamic doc = pdfDocument;
            try
            {
                var prefs = doc.ViewerPreferences;
                Console.WriteLine("Viewer Preferences Overview:");
                Console.WriteLine($"  HideToolbar          : {prefs.HideToolbar}");
                Console.WriteLine($"  HideMenubar          : {prefs.HideMenubar}");
                Console.WriteLine($"  HideWindowUI         : {prefs.HideWindowUI}");
                Console.WriteLine($"  FitWindow            : {prefs.FitWindow}");
                Console.WriteLine($"  CenterWindow         : {prefs.CenterWindow}");
                Console.WriteLine($"  DisplayDocTitle      : {prefs.DisplayDocTitle}");
                Console.WriteLine($"  NonFullScreenPageMode: {prefs.NonFullScreenPageMode}");
                Console.WriteLine($"  Direction            : {prefs.Direction}");
                Console.WriteLine($"  PrintScaling         : {prefs.PrintScaling}");
                Console.WriteLine($"  Duplex               : {prefs.Duplex}");
            }
            catch (RuntimeBinderException)
            {
                // Property not available in the referenced Aspose.Pdf version.
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}
