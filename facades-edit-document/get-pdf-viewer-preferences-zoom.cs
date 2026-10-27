using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ViewerPreferences were introduced in later Aspose.Pdf versions.
            // To keep the source compatible with older versions we access the member
            // via dynamic binding and handle the possible RuntimeBinderException.
            dynamic dynDoc = doc;
            try
            {
                var prefs = dynDoc.ViewerPreferences; // runtime access
                Console.WriteLine($"Page Layout: {prefs.PageLayout}");
                Console.WriteLine($"Zoom Mode: {prefs.Zoom}");

                // When the Zoom mode is Custom, the ZoomFactor property contains the percentage.
                // We compare the string representation to avoid compile‑time dependency on the ZoomMode enum.
                if (prefs.Zoom != null && prefs.Zoom.ToString() == "Custom")
                {
                    Console.WriteLine($"Custom Zoom Factor: {prefs.ZoomFactor}%");
                }
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }

            // Use the Document.Pages collection to obtain the page count (compatible with all versions).
            Console.WriteLine($"Number of pages: {doc.Pages.Count}");
        }
    }
}
