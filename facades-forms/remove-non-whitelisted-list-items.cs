using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the list field "State"
        const string inputPdf  = "FormWithStateList.pdf";
        // Output PDF after removing non‑whitelisted items
        const string outputPdf = "FormWithStateList_Cleaned.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Define the whitelist of allowed state names
        var whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Alabama",
            "Alaska",
            "Arizona",
            "Arkansas",
            "California"
            // add other allowed items here
        };

        // In a real scenario you would retrieve the current items from the list field.
        // For this example we assume a known set of all possible items.
        var allItems = new[]
        {
            "Alabama", "Alaska", "Arizona", "Arkansas", "California",
            "Colorado", "Connecticut", "Delaware", "Florida", "Georgia"
            // ... other states
        };

        // FormEditor works with input and output file paths.
        // It does not implement IDisposable, so we do not wrap it in a using block.
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Delete each item that is not in the whitelist.
        foreach (var item in allItems)
        {
            if (!whitelist.Contains(item))
            {
                // Delete the unwanted item from the "State" list field.
                formEditor.DelListItem("State", item);
            }
        }

        // No explicit Save call is required; the changes are written to outputPdf
        // when the FormEditor operations complete.
        Console.WriteLine($"Cleaned PDF saved to '{outputPdf}'.");
    }
}