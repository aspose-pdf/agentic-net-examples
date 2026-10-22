using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string xfdfPath = "form_data.xfdf";
        const string outputPdf = "roundtrip.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the original PDF and capture all form field values
        var originalValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (var srcDoc = new Document(inputPdf))
        {
            foreach (var field in srcDoc.Form.Fields)
            {
                string value = field.Value?.ToString() ?? string.Empty;
                originalValues[field.FullName] = value;
            }

            // Export the form data to XFDF using the Form facade
            using (var xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                var formFacade = new Form(srcDoc);
                formFacade.ExportXfdf(xfdfStream);
            }
        }

        // Import the XFDF back into a fresh copy of the PDF
        using (var roundTripDoc = new Document(inputPdf))
        {
            // Import XFDF data into the form using the Form facade
            using (var xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                var formFacade = new Form(roundTripDoc);
                formFacade.ImportXfdf(xfdfStream);
            }

            // Verify that each field value matches the original
            bool allMatch = true;
            foreach (var field in roundTripDoc.Form.Fields)
            {
                string fieldName = field.FullName;
                string importedValue = field.Value?.ToString() ?? string.Empty;
                originalValues.TryGetValue(fieldName, out string originalValue);
                if (!string.Equals(importedValue, originalValue, StringComparison.Ordinal))
                {
                    allMatch = false;
                    Console.WriteLine($"Mismatch in field '{fieldName}': original='{originalValue}', imported='{importedValue}'");
                }
            }

            // Save the round‑trip PDF for inspection
            roundTripDoc.Save(outputPdf);
            Console.WriteLine(allMatch
                ? "Round‑trip verification succeeded: all field values preserved."
                : "Round‑trip verification failed: see mismatches above.");
        }
    }
}