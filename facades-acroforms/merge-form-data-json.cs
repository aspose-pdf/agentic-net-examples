using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        string[] pdfFiles = new string[] { "form1.pdf", "form2.pdf", "form3.pdf" };
        List<Dictionary<string, string>> aggregatedData = new List<Dictionary<string, string>>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            using (Document pdfDocument = new Document(pdfPath))
            {
                Dictionary<string, string> fieldValues = new Dictionary<string, string>();
                foreach (Field formField in pdfDocument.Form.Fields)
                {
                    string fieldName = formField.FullName;
                    string fieldValue = formField.Value != null ? formField.Value.ToString() : string.Empty;
                    fieldValues[fieldName] = fieldValue;
                }
                aggregatedData.Add(fieldValues);
            }
        }

        string jsonOutput = JsonSerializer.Serialize(aggregatedData, new JsonSerializerOptions { WriteIndented = true });
        string outputPath = "formdata.json";
        File.WriteAllText(outputPath, jsonOutput);
        Console.WriteLine($"Form data merged to {outputPath}");
    }
}