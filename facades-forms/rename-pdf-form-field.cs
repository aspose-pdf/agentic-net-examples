using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF containing the form field
        const string outputPdf = "output.pdf"; // PDF that will receive the renamed field

        // ------------------------------------------------------------
        // Create a minimal PDF with a form field named "OldName".
        // This makes the example self‑contained and avoids FileNotFoundException.
        // ------------------------------------------------------------
        Document seedDoc = new Document();
        Page seedPage = seedDoc.Pages.Add();
        // Define a rectangle for the text box field (left, bottom, right, top).
        var fieldRect = new Rectangle(100, 700, 200, 750);
        TextBoxField oldField = new TextBoxField(seedPage, fieldRect)
        {
            PartialName = "OldName",
            Value = "Sample value"
        };
        // Add the field to the document's form collection (not directly to page annotations).
        seedDoc.Form.Add(oldField);
        seedDoc.Save(inputPdf);

        // ------------------------------------------------------------
        // Rename the field from "OldName" to "NewName" using FormEditor.
        // Use the non‑obsolete constructor and the Save(string) overload.
        // ------------------------------------------------------------
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPdf);
        formEditor.RenameField("OldName", "NewName");
        formEditor.Save(outputPdf);
    }
}
