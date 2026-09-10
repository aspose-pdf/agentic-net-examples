using System;
using System.IO;
using Aspose.Pdf.Facades;          // Provides the Form class for PDF form operations
using Newtonsoft.Json;            // Json.NET library for deserialization

// Define a C# class that matches the expected JSON structure.
// Property names should correspond to the PDF form field names.
public class FormData
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public int    Age       { get; set; }
    // Add additional properties as needed for your specific form fields.
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use the Aspose.Pdf.Facades.Form class to export form fields to JSON.
        // The Form class implements IDisposable, so wrap it in a using block.
        using (Form form = new Form(pdfPath))
        {
            // ExportJson writes the JSON representation of all form fields to a stream.
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // indented:true makes the JSON output human‑readable.
                form.ExportJson(jsonStream, indented: true);

                // Reset the stream position to the beginning before reading.
                jsonStream.Position = 0;

                // Read the JSON text from the stream.
                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string json = reader.ReadToEnd();

                    // Deserialize the JSON into the strongly‑typed FormData object.
                    FormData data = JsonConvert.DeserializeObject<FormData>(json);

                    // Example usage of the deserialized data.
                    Console.WriteLine($"FirstName: {data?.FirstName}");
                    Console.WriteLine($"LastName:  {data?.LastName}");
                    Console.WriteLine($"Age:       {data?.Age}");
                }
            }
        }
    }
}