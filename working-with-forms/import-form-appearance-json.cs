using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create a simple PDF with a single text box field.
        using (Document sampleDoc = new Document())
        {
            Page page = sampleDoc.Pages.Add();
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField textBox = new TextBoxField(page, fieldRect);
            textBox.PartialName = "NameField";
            textBox.Value = "Default";
            sampleDoc.Form.Add(textBox);
            sampleDoc.Save("input.pdf");
        }

        // Step 2: Write appearance settings to a JSON file.
        string jsonContent = "{\n  \"NameField\": {\n    \"Value\": \"John Doe\",\n    \"DefaultAppearance\": {\n      \"FontName\": \"Helvetica\",\n      \"FontSize\": 12,\n      \"Color\": \"#FF0000\"\n    }\n  }\n}";
        File.WriteAllText("appearance.json", jsonContent);

        // Step 3: Load the PDF and import the JSON appearance settings.
        using (Document doc = new Document("input.pdf"))
        {
            using (FileStream jsonStream = new FileStream("appearance.json", FileMode.Open, FileAccess.Read))
            {
                IEnumerable<Aspose.Pdf.FieldSerializationResult> importResults = doc.Form.ImportFromJson(jsonStream);
                foreach (Aspose.Pdf.FieldSerializationResult result in importResults)
                {
                    Console.WriteLine($"Field: {result.FieldFullName}, Status: {result.FieldSerializationStatus}");
                }
            }
            // Step 4: Save the updated PDF.
            doc.Save("output.pdf");
        }
    }
}