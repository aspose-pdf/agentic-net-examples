using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string exportedXfdf = "data.xfdf";
        const string roundTripPdf = "roundtrip.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        // Export form data to XFDF
        Aspose.Pdf.Facades.Form exportForm = new Aspose.Pdf.Facades.Form(inputPdf);
        using (FileStream exportStream = new FileStream(exportedXfdf, FileMode.Create, FileAccess.Write))
        {
            exportForm.ExportXfdf(exportStream);
        }

        // Capture original field values
        Dictionary<string, string> originalValues = new Dictionary<string, string>();
        foreach (string fieldName in exportForm.FieldNames)
        {
            string value = exportForm.GetField(fieldName);
            originalValues.Add(fieldName, value);
        }

        // Import XFDF into a new PDF copy
        Aspose.Pdf.Facades.Form importForm = new Aspose.Pdf.Facades.Form(inputPdf, roundTripPdf);
        using (FileStream importStream = new FileStream(exportedXfdf, FileMode.Open, FileAccess.Read))
        {
            importForm.ImportXfdf(importStream);
        }
        importForm.Save();

        // Verify that the round‑trip preserved all field values
        Aspose.Pdf.Facades.Form verifyForm = new Aspose.Pdf.Facades.Form(roundTripPdf);
        bool allMatch = true;
        foreach (string fieldName in verifyForm.FieldNames)
        {
            string original = originalValues[fieldName];
            string imported = verifyForm.GetField(fieldName);
            if (original != imported)
            {
                allMatch = false;
                Console.WriteLine($"Mismatch in field '{fieldName}': original='{original}' imported='{imported}'");
            }
        }

        if (allMatch)
        {
            Console.WriteLine("Round‑trip XFDF conversion preserved all field values.");
        }
        else
        {
            Console.WriteLine("Some field values differed after round‑trip conversion.");
        }
    }
}