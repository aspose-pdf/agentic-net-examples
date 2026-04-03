using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // path to the PDF containing the form
        const string fieldName = "MyField";     // name of the form field to read

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the widget annotation representing the field by name
            WidgetAnnotation widget = doc.Form[fieldName];

            // Cast the widget to a Field (all form fields derive from Field)
            Field field = widget as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" not found or is not a form field.");
                return;
            }

            // Export the field's value to a JSON stream (in memory)
            using (MemoryStream ms = new MemoryStream())
            {
                field.ExportValueToJson(ms);          // write JSON representation
                ms.Position = 0;                      // rewind for reading

                using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine($"Field \"{fieldName}\" value (JSON): {json}");
                }
            }
        }
    }
}