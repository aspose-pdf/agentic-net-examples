using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // needed for TextBoxField

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file '{inputPath}' not found.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Fill text fields
            FillFieldOrThrow(pdfDocument, "FirstName", "John");
            FillFieldOrThrow(pdfDocument, "LastName", "Doe");

            // Fill a checkbox field
            FillFieldOrThrow(pdfDocument, "AgreeTerms", true);

            // Save the filled document
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Form filled and saved to '{outputPath}'.");
        }
        catch (InvalidOperationException invEx)
        {
            // Specific handling for missing field errors
            Console.Error.WriteLine($"Form field error: {invEx.Message}");
        }
        catch (FileNotFoundException fnfEx)
        {
            // Handles unexpected missing file errors (e.g., during Save)
            Console.Error.WriteLine($"File error: {fnfEx.Message}");
        }
        catch (Exception ex)
        {
            // General unexpected errors (including file access issues)
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    private static void FillFieldOrThrow(Document doc, string fieldName, string value)
    {
        // Locate the text box field by its full name
        var textField = doc.Form?.Fields?.FirstOrDefault(f => f.FullName == fieldName) as TextBoxField;
        if (textField == null)
        {
            throw new InvalidOperationException($"Text field '{fieldName}' not found in the PDF form.");
        }
        textField.Value = value;
    }

    private static void FillFieldOrThrow(Document doc, string fieldName, bool check)
    {
        // Locate the field (could be a checkbox) by its full name
        var field = doc.Form?.Fields?.FirstOrDefault(f => f.FullName == fieldName);
        if (field == null)
        {
            throw new InvalidOperationException($"Checkbox field '{fieldName}' not found in the PDF form.");
        }

        // Try to set the Checked property via reflection (available on CheckBoxField)
        var checkedProp = field.GetType().GetProperty("Checked");
        if (checkedProp != null && checkedProp.CanWrite)
        {
            checkedProp.SetValue(field, check);
        }
        else
        {
            // Fallback: set the raw value ("On"/"Off" are typical for PDF checkboxes)
            field.Value = check ? "On" : "Off";
        }
    }
}
