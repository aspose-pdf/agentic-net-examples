using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor and bind the source PDF (no full document load into memory)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // Add a single‑line text field
            // Parameters: field type, field name, page number, llx, lly, urx, ury
            editor.AddField(FieldType.Text, "CustomerName", 1, 100, 700, 300, 720);

            // Set a maximum character limit for the text field
            editor.SetFieldLimit("CustomerName", 50);

            // Align text to the left inside the field
            editor.SetFieldAlignment("CustomerName", (int)FormFieldFacade.AlignLeft);

            // Add a checkbox field
            editor.AddField(FieldType.CheckBox, "SubscribeNewsletter", 1, 100, 650, 115, 665);

            // Mark the checkbox as required
            editor.SetFieldAttribute("SubscribeNewsletter", PropertyFlag.Required);

            // NOTE: The original code attempted to use AnnotationFlags.NoPrint, which does not exist in the current Aspose.PDF API.
            // If you need to control the appearance flags, use the available members of AnnotationFlags (e.g., Print, NoView, etc.).
            // For this example the line is omitted to keep the code compiling.
            // editor.SetFieldAppearance("SubscribeNewsletter", AnnotationFlags.NoPrint);

            // Add a submit button that posts form data to a URL
            editor.AddSubmitBtn("SubmitForm", 1, "Submit", "https://example.com/submit", 400, 700, 500, 730);

            // NOTE: SubmitFormFlag.IncludeFormFields is not present in the current API version.
            // The call is removed; if you need to include specific flags, use the members that exist in SubmitFormFlag.
            // editor.SetSubmitFlag("SubmitForm", SubmitFormFlag.IncludeFormFields);

            // Reposition the text field
            editor.MoveField("CustomerName", 100, 600, 300, 620);

            // Rename the checkbox field
            editor.RenameField("SubscribeNewsletter", "NewsletterOptIn");

            // Remove the submit button (demonstrates field deletion)
            editor.RemoveField("SubmitForm");

            // Persist the changes to a new PDF file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Form modifications saved to '{outputPdf}'.");
    }
}
