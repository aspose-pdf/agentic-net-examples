using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "CountryList";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF to read existing items of the list field
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the list box field; cast to ListBoxField
            ListBoxField listField = doc.Form[fieldName] as ListBoxField;
            if (listField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a list box.");
                return;
            }

            // Store current option values (Option.Name) so we can delete them later
            List<string> existingOptions = new List<string>();
            foreach (Option opt in listField.Options)
                existingOptions.Add(opt.Name);

            // Use FormEditor to modify the field. The FormEditor constructor expects a Document,
            // not a file path string. Pass the loaded Document or bind the PDF afterwards.
            using (FormEditor editor = new FormEditor(doc))
            {
                // Remove every existing item
                foreach (string opt in existingOptions)
                {
                    editor.DelListItem(fieldName, opt);
                }

                // Add new items to the list
                string[] newItems = { "USA", "Canada", "Mexico" };
                foreach (string item in newItems)
                {
                    editor.AddListItem(fieldName, item);
                }

                // Persist changes to the destination file (use Save(string) overload).
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"List field '{fieldName}' updated and saved to '{outputPdf}'.");
    }
}
