using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Retrieve the list‑box (or combo‑box) field named "CountryList"
        var listBox = doc.Form["CountryList"] as ListBoxField;
        if (listBox == null)
        {
            Console.Error.WriteLine("Field 'CountryList' not found or is not a list box.");
            return;
        }

        // ------------------------------------------------------------
        // Remove all existing options
        // Options is a read‑only collection; we must delete each entry
        // by its export value (Name).  Collect the names first to avoid
        // modifying the collection while iterating.
        // ------------------------------------------------------------
        var existingNames = listBox.Options
                                   .Cast<Option>()
                                   .Select(o => o.Name)
                                   .ToArray();
        foreach (var name in existingNames)
        {
            listBox.DeleteOption(name);
        }

        // ------------------------------------------------------------
        // Populate the field with the new values
        // Use AddOption(exportValue, displayName) – when only one string
        // is supplied it is used for both export value and display text.
        // ------------------------------------------------------------
        string[] newItems = new[]
        {
            "United States",
            "Canada",
            "Mexico",
            "Germany",
            "France"
        };
        foreach (var item in newItems)
        {
            listBox.AddOption(item);
        }

        // Save the updated PDF
        doc.Save(outputPdf);

        Console.WriteLine($"Dropdown field \"CountryList\" updated and saved to '{outputPdf}'.");
    }
}
