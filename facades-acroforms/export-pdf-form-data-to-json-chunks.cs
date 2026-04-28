using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;                 // For creating a sample PDF when the source file is missing
using Aspose.Pdf.Forms;           // For adding form fields to the sample PDF and accessing field classes
using Aspose.Pdf.Facades;          // For the Form facade used to export form data
using Newtonsoft.Json;            // For JSON parsing and serialization

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";               // Source PDF with AcroForm
        const string outputDir  = "JsonChunks";               // Directory for split JSON files
        const int   maxFields  = 100;                         // Maximum fields per output file

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // 1️⃣ Make sure a PDF with a form actually exists.
        //    If "input.pdf" is not found we create a minimal PDF containing a
        //    single text box field so that the rest of the example can run.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdfWithForm(inputPdf);
            Console.WriteLine($"Sample PDF created at '{inputPdf}'.");
        }

        // ---------------------------------------------------------------------
        // 2️⃣ Bind the PDF to the Form facade and export all fields to a JSON stream
        // ---------------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(inputPdf))
        using (MemoryStream fullJsonStream = new MemoryStream())
        {
            // ExportJson writes the form data as JSON; 'true' enables indentation for readability
            form.ExportJson(fullJsonStream, true);
            fullJsonStream.Position = 0; // Reset stream position for reading

            // Read the entire JSON content
            string fullJson;
            using (StreamReader reader = new StreamReader(fullJsonStream))
            {
                fullJson = reader.ReadToEnd();
            }

            // The exported JSON is an array of field objects.
            // Deserialize into a list of generic dictionaries for easy chunking.
            // JsonConvert.DeserializeObject may return null, so fallback to an empty list.
            List<Dictionary<string, object>> allFields =
                JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(fullJson) ?? new List<Dictionary<string, object>>();

            // -----------------------------------------------------------------
            // 3️⃣ Split the list into chunks of at most 'maxFields' items and write them
            // -----------------------------------------------------------------
            int chunkIndex = 0;
            for (int i = 0; i < allFields.Count; i += maxFields)
            {
                int count = Math.Min(maxFields, allFields.Count - i);
                List<Dictionary<string, object>> chunk = allFields.GetRange(i, count);

                // Serialize the chunk back to JSON (indented for readability)
                string chunkJson = JsonConvert.SerializeObject(chunk, Formatting.Indented);

                // Write the chunk to a separate file
                string chunkPath = Path.Combine(outputDir, $"form_data_{chunkIndex}.json");
                File.WriteAllText(chunkPath, chunkJson);

                chunkIndex++;
            }
        }

        Console.WriteLine("Form data exported and split into JSON files successfully.");
    }

    /// <summary>
    /// Creates a very small PDF containing a single text box form field.
    /// This helper is only used when the expected input file does not exist,
    /// allowing the sample code to run without external resources.
    /// </summary>
    private static void CreateSamplePdfWithForm(string path)
    {
        // Create an empty document with one page
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define a rectangle where the field will be placed (in points)
        // (left, bottom, right, top) – here we use 100, 600, 300, 620
        var rect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

        // Add a text box field named "SampleField"
        TextBoxField txtField = new TextBoxField(page, rect)
        {
            PartialName = "SampleField",
            Value = "Default value"
        };
        doc.Form.Add(txtField, 1);

        // Save the document to the supplied path
        doc.Save(path);
    }
}
