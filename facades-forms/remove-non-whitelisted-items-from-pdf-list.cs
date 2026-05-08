using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "FormWithStateList.pdf";
        const string outputPdf = "FormWithStateList_Whitelisted.pdf";

        // Define the whitelist of allowed state names
        string[] whitelist = new[]
        {
            "Alabama", "Alaska", "Arizona", "Arkansas", "California",
            "Colorado", "Connecticut", "Delaware", "Florida", "Georgia"
            // add other allowed states as needed
        };

        // In a real scenario you would retrieve the existing list items from the PDF.
        // For this example we assume we have an array with all current items.
        string[] allStateItems = new[]
        {
            "Alabama", "Alaska", "Arizona", "Arkansas", "California",
            "Colorado", "Connecticut", "Delaware", "Florida", "Georgia",
            "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa"
            // ... other states present in the list field
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the FormEditor facade for the document
            FormEditor formEditor = new FormEditor(doc);

            // Delete every item that is NOT in the whitelist
            foreach (string item in allStateItems)
            {
                if (!whitelist.Contains(item, StringComparer.OrdinalIgnoreCase))
                {
                    // DelListItem removes the specified item from the list field named "State"
                    formEditor.DelListItem("State", item);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Whitelisted PDF saved to '{outputPdf}'.");
    }
}