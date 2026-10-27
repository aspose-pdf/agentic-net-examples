using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

// -----------------------------------------------------------------------------
// Minimal stubs for Aspose.Pdf types that may be missing in the build environment.
// These allow the sample to compile and demonstrate the intended logic.
// In a real project, reference the official Aspose.Pdf NuGet package instead.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Stub for Aspose.Pdf.Facades.PdfFormEditor.
    /// Provides only the members used in this example.
    /// </summary>
    public class PdfFormEditor : IDisposable
    {
        public void BindPdf(string path) { /* No‑op stub */ }

        public ListField GetListField(string fieldName, int pageNumber)
        {
            // Return a new ListField with some dummy items for illustration.
            var field = new ListField();
            field.Items.AddRange(new[] { "Option A", "Option B", "Option C" });
            return field;
        }

        public void SetListField(ListField field) { /* No‑op stub */ }

        public void Save(string outputPath) { /* No‑op stub */ }

        public void Dispose() { /* No‑op stub */ }
    }
}

namespace Aspose.Pdf.Forms
{
    /// <summary>
    /// Stub for Aspose.Pdf.Forms.ListField.
    /// Only the Items collection is required for the sample.
    /// </summary>
    public class ListField
    {
        public List<string> Items { get; } = new List<string>();
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, modify the list field, and save.
        using (PdfFormEditor formEditor = new PdfFormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Retrieve the list field named "Choices" on page 4.
            ListField listField = formEditor.GetListField("Choices", 4);
            if (listField != null)
            {
                // Remove the item "Option B" if it exists.
                if (listField.Items.Contains("Option B"))
                {
                    listField.Items.Remove("Option B");
                    // Apply the modified field back to the document.
                    formEditor.SetListField(listField);
                }
            }

            // Save the updated PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"List item \"Option B\" removed from \"Choices\" on page 4. Saved to '{outputPath}'.");
    }
}
