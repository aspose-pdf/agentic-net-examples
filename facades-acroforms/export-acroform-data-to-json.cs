using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // Facade API for form operations
using Aspose.Pdf.Forms;            // Form field classes
using Aspose.Pdf.Drawing;          // Rectangle for positioning fields (drawing namespace also contains a Rectangle)
using Newtonsoft.Json;            // Json.NET for deserialization

// Define a typed class that matches the expected JSON structure.
// Properties are nullable to avoid CS8618 warnings when the JSON does not contain a value.
public class MyFormData
{
    public string? FirstName { get; set; }
    public string? LastName  { get; set; }
    public int?    Age       { get; set; }
    // Add additional properties as needed.
}

// Helper class that matches the shape of each element produced by Form.ExportJson().
// ExportJson returns an array where each element contains a "Name" (field name) and a "Value" (field value).
public class ExportedField
{
    [JsonProperty("Name")]
    public string? Name { get; set; }

    [JsonProperty("Value")]
    public string? Value { get; set; }
}

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";      // Source PDF containing the AcroForm
        const string jsonPath = "formdata.json"; // Destination JSON file

        // ------------------------------------------------------------
        // Create a sample PDF with a simple AcroForm (inline seed).
        // ------------------------------------------------------------
        using (Document seedDoc = new Document())
        {
            // Add a page.
            Page page = seedDoc.Pages.Add();

            // Create a text box for FirstName.
            TextBoxField firstNameField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 200, 720))
            {
                PartialName = "FirstName",
                Value = "John"
            };
            seedDoc.Form.Add(firstNameField);

            // Create a text box for LastName.
            TextBoxField lastNameField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 660, 200, 680))
            {
                PartialName = "LastName",
                Value = "Doe"
            };
            seedDoc.Form.Add(lastNameField);

            // Create a text box for Age.
            TextBoxField ageField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 620, 200, 640))
            {
                PartialName = "Age",
                Value = "30"
            };
            seedDoc.Form.Add(ageField);

            // Save the seeded PDF so the Form facade can open it.
            seedDoc.Save(pdfPath);
        }

        // ------------------------------------------------------------
        // Export form fields to JSON using Aspose.Pdf.Facades.Form
        // ------------------------------------------------------------
        // The Form class implements IDisposable, so wrap it in a using block.
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfPath))
        {
            // Create (or overwrite) the JSON file and export the form data.
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // ExportJson writes the field values as JSON.
                // The second argument 'true' enables pretty‑printing (indented output).
                form.ExportJson(jsonStream, true);
            }
        }

        // ------------------------------------------------------------
        // Read the generated JSON and deserialize it into a typed object
        // ------------------------------------------------------------
        // Load the entire JSON content into a string.
        string jsonContent = File.ReadAllText(jsonPath);

        // The JSON produced by ExportJson is an array of {"Name":"...","Value":"..."} objects.
        // Deserialize to a list of ExportedField first.
        List<ExportedField>? exportedFields = JsonConvert.DeserializeObject<List<ExportedField>>(jsonContent);

        if (exportedFields == null)
        {
            Console.WriteLine("Failed to deserialize exported form fields.");
            return;
        }

        // Map the list of ExportedField to the strongly‑typed MyFormData instance.
        MyFormData formData = new MyFormData();
        foreach (var field in exportedFields)
        {
            if (field?.Name == null) continue;
            switch (field.Name)
            {
                case "FirstName":
                    formData.FirstName = field.Value;
                    break;
                case "LastName":
                    formData.LastName = field.Value;
                    break;
                case "Age":
                    if (int.TryParse(field.Value, out int age))
                        formData.Age = age;
                    break;
                // Add additional case blocks for other fields as needed.
            }
        }

        // Example usage of the deserialized data.
        Console.WriteLine($"First Name: {formData.FirstName}");
        Console.WriteLine($"Last Name : {formData.LastName}");
        Console.WriteLine($"Age       : {formData.Age}");
    }
}
