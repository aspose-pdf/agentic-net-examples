using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (replace with your actual file)
        const string inputPdfPath = "input.pdf";

        // Verify that the PDF file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Ensure the document contains a form
            if (pdfDocument.Form == null || pdfDocument.Form.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any AcroForm fields.");
                return;
            }

            // Name of the button field whose value we want to read
            const string buttonFieldName = "MyButton";

            // Retrieve the field by name; the Form collection returns a WidgetAnnotation,
            // so we need to cast it to the base Field type safely.
            Field field = pdfDocument.Form[buttonFieldName] as Field;
            if (field == null)
            {
                Console.WriteLine($"Field '{buttonFieldName}' not found or is not a form field.");
                return;
            }

            // Cast to specific button types
            if (field is ButtonField button)
            {
                // The current value (caption) of the button
                string currentValue = button.Value?.ToString() ?? "(null)";
                Console.WriteLine($"Button field '{buttonFieldName}' current value: {currentValue}");
            }
            else if (field is RadioButtonField radio)
            {
                // For radio buttons, the selected option index (1‑based) indicates the current choice
                int selectedIndex = radio.Selected;
                Console.WriteLine($"Radio button field '{buttonFieldName}' selected index: {selectedIndex}");
            }
            else
            {
                Console.WriteLine($"Field '{buttonFieldName}' is not a recognized button type.");
            }

            // Optional: save the document unchanged (demonstrates the provided save rule)
            const string outputPdfPath = "output.pdf";
            pdfDocument.Save(outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
