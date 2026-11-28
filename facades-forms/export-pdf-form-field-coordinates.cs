using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "coordinates.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(pdfPath);

        // Prepare a dictionary to hold field name -> rectangle coordinates
        var fieldRectangles = new Dictionary<string, object>();

        // Iterate over all form fields in the document
        foreach (var field in doc.Form?.Fields ?? Enumerable.Empty<Field>())
        {
            // Use the field's Rect property to get its position
            var rect = field.Rect;

            // Use FullName if available, otherwise Name
            string fieldName = field.FullName ?? field.Name;

            fieldRectangles[fieldName] = new
            {
                LowerLeftX = rect.LLX,
                LowerLeftY = rect.LLY,
                UpperRightX = rect.URX,
                UpperRightY = rect.URY
            };
        }

        // Serialize the dictionary to JSON with indentation for readability
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(fieldRectangles, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"Field coordinates exported to '{jsonPath}'.");
    }
}
