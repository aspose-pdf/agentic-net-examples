using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "filled_form.pdf";
        const string outputPdf = "updated_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the completed PDF document.
        Document doc = new Document(inputPdf);

        // Retrieve the value of the "Notes" field.
        string notesValue = GetFieldValue(doc, "Notes");

        if (!string.IsNullOrEmpty(notesValue))
        {
            // Copy the value to the "Summary" field.
            bool setResult = SetFieldValue(doc, "Summary", notesValue);
            if (!setResult)
                Console.WriteLine("Field 'Summary' not found.");
        }
        else
        {
            Console.WriteLine("Field 'Notes' not found or empty.");
        }

        // Save the updated PDF.
        doc.Save(outputPdf);
        Console.WriteLine($"Copied 'Notes' to 'Summary' and saved as '{outputPdf}'.");
    }

    /// <summary>
    /// Gets the string value of a form field by its name.
    /// </summary>
    private static string GetFieldValue(Document doc, string fieldName)
    {
        var field = doc?.Form?.Fields?.FirstOrDefault(f =>
            string.Equals(f.FullName, fieldName, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(f.Name, fieldName, StringComparison.OrdinalIgnoreCase));
        return field?.Value?.ToString();
    }

    /// <summary>
    /// Sets the string value of a form field by its name.
    /// Returns true if the field was found and updated.
    /// </summary>
    private static bool SetFieldValue(Document doc, string fieldName, string value)
    {
        var field = doc?.Form?.Fields?.FirstOrDefault(f =>
            string.Equals(f.FullName, fieldName, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(f.Name, fieldName, StringComparison.OrdinalIgnoreCase));
        if (field != null)
        {
            field.Value = value;
            return true;
        }
        return false;
    }
}
