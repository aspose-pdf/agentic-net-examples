using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string jsonData = "data.json";
        const string outputPdf = "filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(jsonData))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonData}");
            return;
        }

        try
        {
            // Load the PDF document (using rule for document creation)
            using (Document doc = new Document(inputPdf))
            {
                // Ensure corrupted objects are ignored during any copy operations
                doc.IgnoreCorruptedObjects = true;

                // Open the JSON file as a stream
                using (FileStream jsonStream = new FileStream(jsonData, FileMode.Open, FileAccess.Read))
                {
                    // Iterate over each form field in the PDF
                    foreach (Field field in doc.Form.Fields)
                    {
                        bool imported = false;
                        try
                        {
                            // Attempt to import the value for the current field.
                            // ImportValueFromJson returns true if the field name exists in the JSON.
                            imported = field.ImportValueFromJson(jsonStream);
                        }
                        catch (Exception ex)
                        {
                            // Log unexpected errors but continue processing other fields.
                            Console.Error.WriteLine($"Error importing field '{field.FullName}': {ex.Message}");
                        }
                        finally
                        {
                            // Reset the stream position for the next field import.
                            jsonStream.Position = 0;
                        }

                        if (!imported)
                        {
                            // Field not present in JSON – skip gracefully.
                            Console.WriteLine($"Field '{field.FullName}' not found in JSON, skipping.");
                        }
                    }
                }

                // Save the filled PDF (using rule for document saving)
                doc.Save(outputPdf);
                Console.WriteLine($"Filled PDF saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}