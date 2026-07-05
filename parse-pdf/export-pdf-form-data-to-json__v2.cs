using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace provides Document and Form classes

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";      // source PDF containing form fields
        const string jsonPath = "formdata.json";  // target JSON file (UTF‑8)

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Export all form fields to a JSON file.
            // The ExportToJson overload writes the JSON using UTF‑8 encoding internally.
            doc.Form.ExportToJson(jsonPath);
        }

        Console.WriteLine($"Form data successfully exported to '{jsonPath}'.");
    }
}