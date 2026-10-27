using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq; // needed for Count()
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for Field types
using Aspose.Pdf.Annotations; // for WidgetAnnotation

class Program
{
    // Simple DTO for JSON serialization
    public class FormFieldInfo
    {
        public string Name { get; set; } = null!;          // non‑nullable, initialized to suppress CS8618
        public string FieldType { get; set; } = null!;    // non‑nullable, initialized to suppress CS8618
        public int PageNumber { get; set; }
        public RectangleCoordinates Rect { get; set; } = null!; // non‑nullable, initialized to suppress CS8618
    }

    public class RectangleCoordinates
    {
        public double LowerLeftX { get; set; }
        public double LowerLeftY { get; set; }
        public double UpperRightX { get; set; }
        public double UpperRightY { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document actually contains a form with fields
            if (pdfDoc.Form == null || pdfDoc.Form.Fields == null || pdfDoc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Build a map of field name -> page number by scanning widget annotations on each page
            var fieldPageMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int pageIdx = 1; pageIdx <= pdfDoc.Pages.Count; pageIdx++)
            {
                Page page = pdfDoc.Pages[pageIdx];
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is WidgetAnnotation widget && !string.IsNullOrEmpty(widget.Name))
                    {
                        // The same field name can appear only once; later occurrences will overwrite which is fine for our purpose
                        fieldPageMap[widget.Name] = pageIdx;
                    }
                }
            }

            var fieldsInfo = new List<FormFieldInfo>();

            // Enumerate each field directly from the Form collection
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Field name
                string name = field.Name;

                // Determine the field type via runtime type name (e.g., TextBoxField, CheckBoxField)
                string fieldType = field.GetType().Name;

                // Page number where the field resides – derived from the widget‑annotation map
                int pageNum = fieldPageMap.TryGetValue(name, out int pn) ? pn : 0; // 0 if not found (should not happen)

                // Rectangle coordinates of the field (field.Rect is never null for a valid form field)
                // Use a fallback rectangle with explicit constructor arguments because Aspose.Pdf.Rectangle has no parameterless ctor.
                Aspose.Pdf.Rectangle rect = field.Rect ?? new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                RectangleCoordinates rectInfo = new RectangleCoordinates
                {
                    LowerLeftX = rect.LLX,
                    LowerLeftY = rect.LLY,
                    UpperRightX = rect.URX,
                    UpperRightY = rect.URY
                };

                fieldsInfo.Add(new FormFieldInfo
                {
                    Name = name,
                    FieldType = fieldType,
                    PageNumber = pageNum,
                    Rect = rectInfo
                });
            }

            // Serialize the list to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldsInfo, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Form field layout exported to '{outputJsonPath}'.");
        }
    }
}
