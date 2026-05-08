using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf API.
        Document pdfDocument = new Document(inputPdf);

        // Update form fields.
        SetFieldValue(pdfDocument, "FirstName", "John");
        SetFieldValue(pdfDocument, "LastName", "Doe");
        SetFieldValue(pdfDocument, "Email", "john.doe@example.com");
        SetCheckBoxValue(pdfDocument, "Subscribe", true);

        // Save the modified PDF.
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Form fields updated and saved to '{outputPdf}'.");
    }

    /// <summary>
    /// Sets the value of a text‑type form field. Works with any field that derives from <see cref="Field"/>.
    /// </summary>
    private static void SetFieldValue(Document doc, string fieldName, string value)
    {
        // Retrieve the field from the form collection.
        var field = doc.Form[fieldName] as Field;
        if (field == null)
        {
            Console.Error.WriteLine($"Field '{fieldName}' not found.");
            return;
        }

        // For text boxes the Value property can be set directly.
        field.Value = value;
    }

    /// <summary>
    /// Sets the checked state of a checkbox field without relying on the concrete <c>CheckBoxField</c> type.
    /// The underlying PDF representation expects the value "On" for a checked box and "Off" for an unchecked one.
    /// </summary>
    private static void SetCheckBoxValue(Document doc, string fieldName, bool isChecked)
    {
        var field = doc.Form[fieldName] as Field;
        if (field == null)
        {
            Console.Error.WriteLine($"Checkbox field '{fieldName}' not found.");
            return;
        }

        // The generic Field class does not expose a "Checked" property, so we set the raw value.
        field.Value = isChecked ? "On" : "Off";
    }
}
