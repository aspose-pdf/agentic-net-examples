using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // Added to resolve TextFragment reference

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "combined.pdf";

        // Ensure the source PDF exists – if not, create a minimal one with the required field.
        if (!File.Exists(sourcePath))
        {
            CreateSampleSource(sourcePath);
        }

        // Load the source PDF into a Document object
        Document doc = new Document(sourcePath);

        // Make sure there is a second page to host the copied field.
        if (doc.Pages.Count < 2)
        {
            doc.Pages.Add();
        }

        // Retrieve the existing field "EmployeeID" from the form.
        // The field is expected to be a TextBoxField; cast accordingly.
        TextBoxField originalField = doc.Form["EmployeeID"] as TextBoxField;
        // Use null‑coalescing to guarantee a non‑null string value.
        string employeeIdValue = (originalField?.Value?.ToString()) ?? string.Empty;

        // Create a new TextBoxField named "EmployeeIDCopy" on page 2.
        // Define the rectangle where the field will appear (llx, lly, urx, ury).
        Rectangle rect = new Rectangle(100, 500, 200, 520);
        TextBoxField copyField = new TextBoxField(doc.Pages[2], rect)
        {
            PartialName = "EmployeeIDCopy",
            Value = employeeIdValue
        };

        // Add the new field to the form on page 2 (page numbers are 1‑based).
        doc.Form.Add(copyField, 2);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Field copied and saved to '{outputPath}'.");
    }

    // Helper: creates a simple PDF with a single TextBoxField called "EmployeeID".
    private static void CreateSampleSource(string path)
    {
        using (Document sample = new Document())
        {
            Page page = sample.Pages.Add();
            // Add a visible label (optional)
            page.Paragraphs.Add(new TextFragment("Employee ID:"));
            // Define field rectangle
            Rectangle fieldRect = new Rectangle(150, 500, 250, 520);
            TextBoxField field = new TextBoxField(page, fieldRect)
            {
                PartialName = "EmployeeID",
                Value = "12345"
            };
            sample.Form.Add(field, 1);
            sample.Save(path);
        }
    }
}
