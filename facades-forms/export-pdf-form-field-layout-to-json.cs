using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "fields_layout.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use the Form class from Aspose.Pdf.Facades explicitly to avoid ambiguity with Aspose.Pdf.Forms.Form
        using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form(inputPdf))
        {
            // Access the underlying Document object
            Document doc = pdfForm.Document;

            // Collect layout information for each form field
            var fieldInfos = new List<object>();

            // Iterate over the fields defined in the PDF form
            foreach (Field field in doc.Form.Fields)
            {
                // Get the field rectangle (account for page rotation)
                Aspose.Pdf.Rectangle rect = field.GetRectangle(true);

                // PageIndex is 1‑based in Aspose.Pdf
                int pageNumber = field.PageIndex;

                fieldInfos.Add(new
                {
                    Name = field.FullName,
                    Page = pageNumber,
                    Rect = new
                    {
                        LLX = rect.LLX,
                        LLY = rect.LLY,
                        URX = rect.URX,
                        URY = rect.URY
                    }
                });
            }

            // Serialize the layout data to indented JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldInfos, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Form field layout exported to '{outputJson}'.");
    }
}
