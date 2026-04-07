using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fieldName = "MyField"; // replace with the actual field name

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // The Form indexer returns a WidgetAnnotation. Cast it to a Field.
                Field field = doc.Form[fieldName] as Field;
                if (field == null)
                {
                    Console.WriteLine($"Field \"{fieldName}\" not found.");
                    return;
                }

                // Handle known field types explicitly.
                if (field is TextBoxField textBox)
                {
                    string value = textBox.Value;
                    Console.WriteLine($"Field \"{fieldName}\" value: {value}");
                }
                else if (field is CheckboxField checkBox)
                {
                    bool isChecked = checkBox.Checked;
                    Console.WriteLine($"Field \"{fieldName}\" checked: {isChecked}");
                }
                else if (field is RadioButtonField radioButton)
                {
                    string selected = radioButton.Value;
                    Console.WriteLine($"Field \"{fieldName}\" selected value: {selected}");
                }
                else
                {
                    // For other field types, export the value to JSON.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        field.ExportValueToJson(ms);
                        ms.Position = 0;
                        using (StreamReader reader = new StreamReader(ms))
                        {
                            string json = reader.ReadToEnd();
                            Console.WriteLine($"Field \"{fieldName}\" JSON export: {json}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
