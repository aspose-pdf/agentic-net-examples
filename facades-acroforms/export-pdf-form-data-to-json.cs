using System;
using System.IO;
using Aspose.Pdf.Facades;          // Form class resides here
using Newtonsoft.Json;            // Json.NET for deserialization

// Define a typed class that matches the JSON structure of the exported form fields.
// Adjust property names/types to correspond to the actual field names in the PDF form.
public class FormData
{
    // Example fields – replace with actual field names from your PDF form.
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email     { get; set; }
    public bool   Subscribe { get; set; }
    // Add additional properties as needed.
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input_form.pdf";

        // Ensure the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Export form fields to JSON using Aspose.Pdf.Facades.Form.
        // The Form class implements IDisposable, so wrap it in a using block.
        using (Form form = new Form(inputPdfPath))
        {
            // Use a memory stream to capture the JSON output.
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // ExportJson writes the JSON representation of all form fields.
                // The second argument 'true' enables pretty‑printing (indented output).
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
                    Console.WriteLine("Deserialized form data:");
                    Console.WriteLine($"FirstName: {data.FirstName}");
                    Console.WriteLine($"LastName : {data.LastName}");
                    Console.WriteLine($"Email    : {data.Email}");
                    Console.WriteLine($"Subscribe: {data.Subscribe}");
                }
            }
        }
    }
}