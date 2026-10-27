using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace PdfFormBatchProcessor
{
    // Simple DTO to hold form data for each PDF
    public class FormData
    {
        public string FileName { get; set; }
        public Dictionary<string, string> Fields { get; set; }

        // Constructor ensures non‑null properties for C# 8+ nullable reference types
        public FormData(string fileName, Dictionary<string, string> fields)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }
    }

    class Program
    {
        static void Main()
        {
            // Folder containing the source PDFs
            const string inputFolder = @"C:\InputPdfs";
            // Path for the resulting JSON file
            const string outputJsonPath = @"C:\Output\combinedFormData.json";

            // Collect all PDF files in the folder
            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

            // List that will become the JSON array
            List<FormData> batchData = new List<FormData>();

            foreach (string pdfPath in pdfFiles)
            {
                // Ensure each Document is disposed promptly
                using (Document doc = new Document(pdfPath))
                {
                    // Prepare a dictionary for the current PDF's form fields
                    Dictionary<string, string> fieldValues = new Dictionary<string, string>();

                    // Iterate over all form fields in the document using the correct Field type
                    foreach (Field field in doc.Form.Fields)
                    {
                        // PartialName uniquely identifies the field; Value may be null
                        string name = field.PartialName;
                        string value = field.Value?.ToString() ?? string.Empty;
                        fieldValues[name] = value;
                    }

                    // Add the extracted data to the batch list
                    batchData.Add(new FormData(
                        Path.GetFileName(pdfPath),
                        fieldValues
                    ));
                }
            }

            // Serialize the batch list to a pretty‑printed JSON string
            string json = JsonSerializer.Serialize(
                batchData,
                new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the output file
            File.WriteAllText(outputJsonPath, json);

            Console.WriteLine($"Combined form data saved to '{outputJsonPath}'.");
        }
    }
}
