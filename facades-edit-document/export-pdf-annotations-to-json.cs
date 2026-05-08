using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class ExportAnnotationsToJson
{
    static void Main()
    {
        // Input PDF file containing annotations
        const string inputPdfPath = "input.pdf";
        // Output JSON file that will hold the exported annotation data
        const string outputJsonPath = "annotations.json";

        // Ensure the input file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Use PdfAnnotationEditor (Facade) to work with PDF annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPdfPath);

            // Export all annotations to XFDF using an in‑memory stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset stream for reading

                // Load the XFDF XML
                XDocument xfdfDoc = XDocument.Load(xfdfStream);

                // Prepare a simple list of dictionaries to hold annotation data
                var annotations = new List<Dictionary<string, string>>();

                // XFDF structure: <xfdf><annots><...annotation elements.../></annots></xfdf>
                foreach (var annotElement in xfdfDoc.Descendants("annots").Elements())
                {
                    var dict = new Dictionary<string, string>();
                    foreach (var child in annotElement.Elements())
                    {
                        // Store each child element's name and value
                        dict[child.Name.LocalName] = child.Value;
                    }
                    annotations.Add(dict);
                }

                // Serialize the annotation list to formatted JSON
                string json = JsonSerializer.Serialize(
                    annotations,
                    new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to the output file
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Annotations exported to JSON: {outputJsonPath}");
            }
        }
    }
}