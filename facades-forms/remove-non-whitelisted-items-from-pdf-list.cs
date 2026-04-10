using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists before attempting to edit it.
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"The file '{inputPdf}' was not found. Please provide a valid PDF file.");
            return;
        }

        // Whitelist of allowed items for the "State" list field
        var whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Alabama",
            "Alaska",
            "Arizona",
            "Arkansas"
            // Add other allowed states as needed
        };

        // Complete list of possible items in the "State" list field
        var allStates = new[]
        {
            "Alabama","Alaska","Arizona","Arkansas","California","Colorado","Connecticut","Delaware",
            "Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky",
            "Louisiana","Maine","Maryland","Massachusetts","Michigan","Minnesota","Mississippi",
            "Missouri","Montana","Nebraska","Nevada","New Hampshire","New Jersey","New Mexico",
            "New York","North Carolina","North Dakota","Ohio","Oklahoma","Oregon","Pennsylvania",
            "Rhode Island","South Carolina","South Dakota","Tennessee","Texas","Utah","Vermont",
            "Virginia","Washington","West Virginia","Wisconsin","Wyoming"
        };

        // Load the PDF document first (FormEditor expects a Document, not a file path)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize FormEditor with the loaded Document instance
            using (FormEditor formEditor = new FormEditor(pdfDocument))
            {
                // Delete items not present in the whitelist
                foreach (var state in allStates)
                {
                    if (!whitelist.Contains(state))
                    {
                        formEditor.DelListItem("State", state);
                    }
                }
            }

            // Save the modified PDF to the desired output location
            pdfDocument.Save(outputPdf);
        }
    }
}
