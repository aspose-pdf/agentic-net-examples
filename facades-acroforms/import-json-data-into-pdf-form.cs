using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "template.pdf";   // PDF with form fields
        const string outputPdfPath = "filled.pdf";     // Result PDF
        const string jsonPath      = "data.json";      // JSON containing field values

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load the PDF form using the Facades Form class.
        // The constructor (string inputFile, string outputFile) prepares the form for editing.
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Retrieve the list of field names that actually exist in the PDF.
            var pdfFieldNames = form.FieldNames.ToHashSet(StringComparer.Ordinal);

            // Load the entire JSON document.
            JObject fullJson;
            using (FileStream jsonFs = File.OpenRead(jsonPath))
            using (StreamReader sr = new StreamReader(jsonFs))
            {
                fullJson = JObject.Parse(sr.ReadToEnd());
            }

            // Build a new JSON object that contains only the keys present in the PDF.
            JObject filteredJson = new JObject();
            foreach (var property in fullJson.Properties())
            {
                if (pdfFieldNames.Contains(property.Name))
                {
                    filteredJson.Add(property.Name, property.Value);
                }
            }

            // Write the filtered JSON to a memory stream for ImportJson.
            using (MemoryStream filteredStream = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(filteredStream))
            {
                sw.Write(filteredJson.ToString());
                sw.Flush();
                filteredStream.Position = 0; // Reset stream position before reading.

                // Import the filtered JSON into the PDF form.
                form.ImportJson(filteredStream);
            }

            // Save the updated PDF. The Form class inherits SaveableFacade,
            // so calling Save() writes the output file specified in the constructor.
            form.Save();
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdfPath}'.");
    }
}