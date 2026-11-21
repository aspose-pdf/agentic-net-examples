using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        // Path to the PDF whose viewer preferences will be read and modified
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(pdfPath);

        // Use dynamic access to ViewerPreferences to stay compatible with older Aspose.Pdf versions.
        dynamic doc = pdfDocument;
        dynamic prefs;
        try
        {
            prefs = doc.ViewerPreferences;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            return;
        }

        // Map the boolean viewer preferences to UI toggle controls (simulated with a dictionary).
        // Use string literals for the keys because the ViewerPreferences type may not be available at compile time.
        var uiToggles = new Dictionary<string, bool>
        {
            { "HideToolbar",   (bool)prefs.HideToolbar },
            { "HideMenubar",   (bool)prefs.HideMenubar },
            { "HideWindowUI",  (bool)prefs.HideWindowUI },
            { "FitWindow",     (bool)prefs.FitWindow },
            { "CenterWindow",  (bool)prefs.CenterWindow },
            { "DisplayDocTitle", (bool)prefs.DisplayDocTitle }
        };

        // Display current toggle states (in a real UI these would be bound to checkboxes, switches, etc.)
        Console.WriteLine("Current Viewer Preference Toggles:");
        foreach (var kvp in uiToggles)
        {
            Console.WriteLine($"{kvp.Key}: {(kvp.Value ? "On" : "Off")}");
        }

        // -----------------------------------------------------------------
        // Simulate a user changing some toggles at runtime.
        // For example, toggle the visibility of the toolbar and enable FitWindow.
        // -----------------------------------------------------------------
        uiToggles["HideToolbar"] = !uiToggles["HideToolbar"];
        uiToggles["FitWindow"] = true;

        // Apply the modified toggle values back to the ViewerPreferences object
        prefs.HideToolbar   = uiToggles["HideToolbar"];
        prefs.FitWindow     = uiToggles["FitWindow"];
        // (Other toggles can be updated similarly if needed)

        // Save the updated viewer preferences back into the PDF
        pdfDocument.Save(pdfPath);

        Console.WriteLine("\nUpdated Viewer Preference Toggles:");
        foreach (var kvp in uiToggles)
        {
            Console.WriteLine($"{kvp.Key}: {(kvp.Value ? "On" : "Off")}");
        }

        Console.WriteLine("\nViewer preferences have been applied to the PDF.");
    }
}
