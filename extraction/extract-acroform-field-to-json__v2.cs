using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormB.pdf";
        const string jsonPath = "FormB_CustomerName.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Retrieve the AcroForm field named "CustomerName"
                var field = doc.Form["CustomerName"] as TextBoxField;
                if (field == null)
                {
                    Console.Error.WriteLine("Field 'CustomerName' not found or is not a text box.");
                    return;
                }

                string value = field.Value;

                // Serialize the value to JSON
                string json = JsonSerializer.Serialize(
                    new { CustomerName = value },
                    new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(jsonPath, json);
                Console.WriteLine($"Customer name saved to {jsonPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}