using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // ------------------------------------------------------------
        // Ensure a PDF with at least one form field exists.
        // If the file is missing, create a minimal PDF and add a
        // TextBox form field named "SampleTextBox".
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            var doc = new Document();
            var page = doc.Pages.Add();

            // Define the rectangle for the form field (left, bottom, right, top).
            var fieldRect = new Rectangle(100, 600, 200, 650);
            var textBox = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleTextBox",
                Value = "Default"
            };
            doc.Form.Add(textBox);
            doc.Save(inputPdf);
        }

        // ------------------------------------------------------------
        // Load the PDF with FormEditor and list all form field names.
        // ------------------------------------------------------------
        using (var formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Access the underlying Document.
            var doc = formEditor.Document;

            // Retrieve field names via the Form.Fields collection.
            string[] fieldNames = doc?.Form?.Fields?
                .Select(f => f.Name)
                .ToArray() ?? Array.Empty<string>();

            foreach (var name in fieldNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}
