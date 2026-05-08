using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string flattenedPdfPath = "flattened.pdf";   // PDF that was previously flattened
        const string originalPdfPath  = "original.pdf";    // Original PDF before flattening
        const string outputPdfPath   = "restored.pdf";    // PDF where we will continue editing

        // Verify that the flattened file exists
        if (!File.Exists(flattenedPdfPath))
        {
            Console.Error.WriteLine($"Flattened PDF not found: {flattenedPdfPath}");
            return;
        }

        // Verify that the original (unflattened) file exists – flattening is irreversible,
        // so the only way to "unflatten" is to reload the original document.
        if (!File.Exists(originalPdfPath))
        {
            Console.Error.WriteLine($"Original PDF not found: {originalPdfPath}");
            return;
        }

        // Load the original (unflattened) document, modify it as needed, and save.
        // This restores editability because the original form fields are still present.
        using (Document doc = new Document(originalPdfPath))
        {
            // Example modification: set a value for a text field named "Name"
            // Use the Field object (Aspose.Pdf.Forms.Field) and its Value property.
            Field nameField = doc.Form["Name"] as Field;
            if (nameField != null)
            {
                nameField.Value = "John Doe";
            }

            // Save the modified document – further edits can now be performed.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Restored PDF with editable form fields saved to '{outputPdfPath}'.");
    }
}
